using SAPAutomation.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Young.Data;
using Young.Data.Attributes;

namespace SAPComponentReflect
{
    public class SAPReflect
    {
        public static IEnumerable<Type> GetSAPScreenFromAssembly(Assembly asm)
        {
            return asm.GetTypes().Where(t => t.IsSubclassOf(typeof(SAPGuiScreen))).OrderBy(t=>t.Name);
        }

        public static IEnumerable<Tuple<string, bool>> DisplayData(Type t)
        {
            List<Tuple<string, bool>> members = new List<Tuple<string, bool>>();

            foreach (var p in t.GetProperties())
            {
                bool isBinding = false;
                if(p.GetCustomAttribute(typeof(ColumnBindingAttribute),true)!=null)
                {
                    isBinding = true;
                }
                Tuple<string, bool> member = new Tuple<string, bool>(p.Name, isBinding);
                members.Add(member);
            }

            foreach (var m in t.GetMethods().Where(m=>m.IsPublic && m.IsSpecialName == false))
            {
                bool isBinding = false;
                if(m.GetCustomAttribute(typeof(MethodBindingAttribute),true)!=null)
                {
                    isBinding = true;
                }
                string method = string.Empty;
                method += m.ReturnType.Name + " " + m.Name;

                ParameterInfo[] paInfoes = m.GetParameters();
                if (paInfoes.Count() > 0)
                {
                    method += "(";
                    foreach (var p in paInfoes)
                    {
                        if (p.IsOptional)
                        {
                            method += "[Optional]";
                        }
                        method += p.ParameterType.Name + " " + p.Name + ",";
                    }
                    method = method.Substring(0, method.Length - 1);
                    method += ")";
                }
                else
                {
                    method += "()";
                }
                Tuple<string, bool> member = new Tuple<string, bool>(method, isBinding);
                members.Add(member);
            }

            return members;


        }
    }

    

}
