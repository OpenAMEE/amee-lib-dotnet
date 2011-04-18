using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace BergenAmee.Model
{
    public class DimEmissionCategories
    {
        public int DimCommodityID { get; set; }
        public String ameeDataPath { get; set; }
        public String ameeDrillDownURL { get; set; }

        public static Dictionary<int, DimEmissionCategories> ameeDataPathDrillDown = new Dictionary<int, DimEmissionCategories>();

        /*
        public static void init(SqlConnection connection)
        {
            SqlDataReader sdr = null;

            // Connect to the database and get all meters without result
            SqlCommand sqlSelect = new SqlCommand(@"SELECT [DimCommodityID], [ameeDataPath], [ameeDrillDownURL]
                    FROM DimEmissionCategories;"
                    );
            sdr = DataBaseHelper.executeRead(connection, sqlSelect);
            while (sdr.Read())
            {
                DimEmissionCategories dimCat = new DimEmissionCategories();
                
                int idCommodity = sdr.GetInt32(0);

                dimCat.DimCommodityID = idCommodity;
                dimCat.ameeDataPath = sdr.GetString(1);
                dimCat.ameeDrillDownURL = sdr.GetString(2);

                ameeDataPathDrillDown.Add(idCommodity, dimCat);

            }
            sdr.Close();
        }
        */
    }
}
