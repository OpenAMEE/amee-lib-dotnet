using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace BergenAmee.Model
{
    /// <summary>
    /// Mapping with the table DimCommodities.
    /// </summary>
    public class DimCommodities
    {
        public const int Electricity = 1;          //kWh
        public const int Oil = 3;                  //m3
        public const int Water = 4;                //m3
        public const int Coal = 5;                 //T
        public const int GridLoss = 7;             //kWh
        public const int CO2Certificate = 8;       //m3
        public const int ElCertificate = 9;        //kWh
        public const int Waste = 10;               //kg
        public const int DistrictHeating = 11;     //kWh
        public const int DistrictCooling = 12;     //kWh
        public const int Green = 13;               //kWh
        public const int Fondskraft = 14;          //kWh
        public const int WasteWater = 15;          //m3
        public const int GridTransport = 16;       //kWh
        public const int Broadband = 17;           //NULL
        public const int ADMCost = 18;             //day
        public const int ContractCleaning = 19;    //NULL

        // below added by Zen'to
        public const int NaturalGas = 21;                       //kWh
        public const int LPG = 22;                              //m3
        public const int Butane = 23;                           //m3
        public const int HeatingOil = 24;                       //m3
        //public const int FugitiveCO2 = 25;                      //m3
        //public const int FugitiveCH4 = 26;                      //m3
        //public const int FugitiveN2O = 27;                      //m3
        //public const int FugitiveHFC = 28;                      //m3
        //public const int FugitivePFC = 29;                      //m3
        //public const int FugitiveSF = 30;                       //m3
        public const int Propane = 31;                          //m3
        public const int AirTravel = 32;                        //km        --
        public const int Taxi = 33;                             //km
        public const int Train = 34;                            //km
        public const int Bus = 35;                              //km
        public const int Boat = 36;                             //km
        public const int WasteRecycling = 37;                   //Ton

        public const int FreightAir = 38;                       //Ton km    ---
        public const int FreightRoad = 39;                      //Ton km
        public const int FreightTrain = 40;                     //Ton km
        public const int FreightBoat = 41;                      //Ton km

        public const int RentalOrPrivateVehiculeCar = 42;       //km
        public const int RentalOrPrivateVehiculeVan = 43;       //km
        public const int RentalOrPrivateVehiculeTruck = 44;     //km

        public const int CompanyVehicleMileageCar = 45;         //km
        public const int CompanyVehicleMileageVan = 46;         //km
        public const int CompanyVehicleMileageTruck = 47;       //km

        public const int CompanyVehicleFuelPetrol = 48;         //L
        public const int CompanyVehicleFuelDiesel = 49;         //L
        public const int CompanyVehicleFuelLPG = 50;            //L
        public const int CompanyVehicleFuelNaturalGas = 51;     //L

        public const int Biomass = 52;                          //Ton
        public const int FugitiveEmission = 53;                 //m3

        public static AMEERequest getAMEERequest(int commodityId, String countryName)
        {
            return getAMEERequest(commodityId, null, 0, null, null, null, countryName);
        }


        public static AMEERequest getAMEERequest(int commodityId, String dataItemUid, decimal volume, String unit, String startDate, String endDate, String countryName)
        {
            AMEERequest aMEERequest = new AMEERequest();

            String body = null;
            String strVolume = null;
            if (dataItemUid != null)
            {
                strVolume = "" + volume;
                strVolume = strVolume.Replace(',', '.');
                body = "representation=full&name=bergen&dataItemUid=" + dataItemUid;
            }

            DimEmissionCategories dimCat = DimEmissionCategories.ameeDataPathDrillDown[commodityId];

            aMEERequest.dataPath = dimCat.ameeDataPath;
            aMEERequest.dataPathDrillDown = dimCat.ameeDataPath + dimCat.ameeDrillDownURL;

            switch (commodityId)
            {
                // 1
                case DimCommodities.Electricity:
                    if (body != null)
                    {
                        body += getBodyBusinessEnergyElectricity(strVolume, unit);
                    }
                    aMEERequest.dataPathDrillDown += countryName;
                    break;

                // 3 21 22 23 24 31
                case DimCommodities.Oil: 
                case DimCommodities.NaturalGas:
                case DimCommodities.LPG:
                case DimCommodities.Butane:
                case DimCommodities.HeatingOil:
                case DimCommodities.Propane:
                case DimCommodities.Coal:
                case DimCommodities.Biomass:
                    if (body != null)
                    {
                        body += getBodyBusinessEnergyStationaryCombustion(strVolume, unit);
                    }
                    break;

                //53
                case DimCommodities.FugitiveEmission:
                    if (body != null)
                    {
                        body += getBodyFugitiveEmissions(strVolume, unit);
                    }
                    break;

                case DimCommodities.AirTravel:                      //32
                    if (body != null)
                    {
                        body += getBodyTransportGhgpPassenger(strVolume, unit);
                    }
                    break;
                
                // 33 34 35 36
                case DimCommodities.Taxi:
                case DimCommodities.Train:
                case DimCommodities.Bus:
                case DimCommodities.Boat:
                    if (body != null)
                    {
                        body += getBodyTransportGhgpPassenger(strVolume, unit);
                    }
                    break;

                // 38 39 40 41
                case DimCommodities.FreightAir:
                case DimCommodities.FreightRoad:
                case DimCommodities.FreightTrain:
                case DimCommodities.FreightBoat:
                    if (body != null)
                    {
                        body += getBodyTransportGhgpFreight(strVolume, unit);
                    }
                    break;

                // 42 43 44 45 46 47
                case DimCommodities.RentalOrPrivateVehiculeCar:
                case DimCommodities.RentalOrPrivateVehiculeVan:
                case DimCommodities.RentalOrPrivateVehiculeTruck:
                case DimCommodities.CompanyVehicleMileageCar:
                case DimCommodities.CompanyVehicleMileageVan:
                case DimCommodities.CompanyVehicleMileageTruck:
                    if (body != null)
                    {
                        body += getBodyTransportGhgpVehicleOther(strVolume, unit);
                    }
                    break;

                // 48 49 50 51
                case DimCommodities.CompanyVehicleFuelPetrol:
                case DimCommodities.CompanyVehicleFuelDiesel:
                case DimCommodities.CompanyVehicleFuelLPG:
                case DimCommodities.CompanyVehicleFuelNaturalGas:
                    if (body != null)
                    {
                        body += getBodyTransportGhgpFuel(strVolume, unit);
                    }
                    break;

                default:
                    throw new Exception("This commodity ID [" + commodityId + "] is not yet supported");
            }

            if (startDate != null)
            {
                body += "&startDate=" + startDate;
            }
            if (endDate != null)
            {
                body += "&endDate=" + endDate;
            }

            aMEERequest.body = body;

            return aMEERequest;
        }

        private static string getBodyBusinessEnergyElectricity(String volume, string unit)
        {
            return "&energyPerTime=" + volume + "&energyUnit=" + unit;
        }

        private static string getBodyBusinessEnergyStationaryCombustion(String volume, string unit)
        {
            if (unit == "t")
            {
                return "&mass=" + volume + "&massUnit=" + unit;
            }
            else if (unit == "kWh")
            {
                return "&NRG=" + volume + "&NRGUnit=" + unit;
            }
            else
            {
                return "&volume=" + volume + "&volumeUnit=" + unit;
            }
        }

        private static string getBodyTransportGhgpPassenger(string distance, string unit)
        {
            // TODO passengers is static
            return "&distance=" + distance + "&distanceUnit=" + unit + "&passengers=1";
        }

        private static string getBodyTransportGhgpFreight(string distance, string unit)
        {
            // unit is Tkm so put km and t
            return "&distance=" + distance + "&distanceUnit=km&mass=1&massUnit=t";
        }
        private static string getBodyFugitiveEmissions(String volume, string unit)
        {
            // TODO month is static
            return "&emissionRate=" + volume + "&volumeUnit=" + unit + "&volumePerUnit=month";
        }

        private static string getBodyTransportGhgpVehicleOther(String volume, string unit)
        {
            return "&distance=" + volume + "&distanceUnit=" + unit;
        }

        private static string getBodyTransportGhgpFuel(String volume, string unit)
        {
            return "&volume=" + volume + "&volumeUnit=" + unit;
        }
    }
}
