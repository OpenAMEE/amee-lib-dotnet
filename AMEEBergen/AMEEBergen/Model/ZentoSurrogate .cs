using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Collections.ObjectModel;

namespace BergenAmee.Model
{
    public class ZentoSurrogate : IDataContractSurrogate
    {
        public Type GetDataContractType(Type type)
        {
            return type;
        }
        public object GetObjectToSerialize(object obj, Type targetType)
        {
            return obj;
        }
        public object GetDeserializedObject(object obj, Type targetType)
        {
            return obj;
        }
        public Type GetReferencedTypeOnImport(string typeName, string typeNamespace, object customData)
        {
            return null;
        }

        public System.CodeDom.CodeTypeDeclaration ProcessImportedType(System.CodeDom.CodeTypeDeclaration typeDeclaration, System.CodeDom.CodeCompileUnit compileUnit)
        {
            return typeDeclaration;
        }


        public object GetCustomDataToExport(Type clrType, Type dataContractType)
        {
            return null;
        }

        public object GetCustomDataToExport(System.Reflection.MemberInfo memberInfo, Type dataContractType)
        {
            return null;
        }

        public void GetKnownCustomDataTypes(Collection<Type> customDataTypes)
        {
        }

    }
}
