using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;

using SnapTextWeb.DAL;
using SnapTextWeb.Models;

namespace SnapTextWeb.Extensions
{
    public class Current
    {
        public static IPrincipal User
        {
            get
            {
                return System.Threading.Thread.CurrentPrincipal;
            }
            set
            {
                System.Threading.Thread.CurrentPrincipal = value;
            }
        }

        public static string UserId
        {
            get
            {
                return User.Identity.GetUserId();
            }
        }

        public static dynamic UserInfo
        {
            get
            {
                // Instantiate dictionary.
                var result = new Dictionary<string, object>();

                if (User.Identity.IsAuthenticated)
                {
                    var claimsIdentity = User.Identity as ClaimsIdentity;

                    //claimsIdentity.Claims.ToList().ForEach(x => result.Add(x..ToLower(), x.Value, x.DataType));
                    //var keys = result.Keys;

                    foreach (var claim in claimsIdentity.Claims)
                    {
                        switch (claim.Type)
                        {
                            case "FirstName":
                            case "LastName":
                                result.Add(claim.Type, claim.Value);
                                break;
                            default:
                                break;
                        }
                    }
                }

                DynamicDictionary dd = new DynamicDictionary(result);
                return dd;
                //return new DynamicDictionary(result);
            }
        }
    }

    public class DynamicDictionary : DynamicObject
    {
        private readonly Dictionary<string, object> dictionary;

        public DynamicDictionary(Dictionary<string, object> dictionary)
        {
            this.dictionary = dictionary;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            //var key = binder.Name;
            //if (dictionary.ContainsKey(key))
            //{
            //    result = dictionary[key];
            //    return true;
            //}
            //throw new KeyNotFoundException(string.Format("Key \"{0}\" was not found in the given dictionary", key));
            
            //return dictionary.TryGetValue(binder.Name, out result);

            result = dictionary.ContainsKey(binder.Name) ? dictionary[binder.Name] : string.Empty;
            return true;
        }

        //public override bool TrySetMember(SetMemberBinder binder, object value)
        //{
        //    dictionary[binder.Name.ToLower()] = value;

        //    return true;
        //}
    }
}