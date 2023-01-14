using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authen
{
    internal class ClaimExample
    {
    }

    public class Claim1
    {
        public Claim1(string type, string value, string? valueType = null)
        {
            Type = type;
            Value = value;
            ValueType = valueType;
        }

        public string Type { get; set; }
        public string Value { get; set; }
        public string ValueType { get; set; }
    }

    public class Claims1Identity
    {
        public string Name { get; set; }
        public IEnumerable<Claim1> Claims { get; set;}
        public string AuthenticationType { get; set; }
        public  bool IsAuthenticated { get; set; }
    }
    public class Claims1Principal
    {
        public IEnumerable<Claim1> Claims { get; set; }
        public IEnumerable<Claims1Identity> claims1Identities { get; set; }
        public Claims1Identity Identity { get; set; }
    }

  public class IdentityExample
    {
        public static Claim1[] userClaims()
        {
            var idClaim = new Claim1("Id", "1", "Integer");
            var dobClaim = new Claim1("dob", "2021/10/10", "Date");
            var roleClaim = new Claim1("Role", "Admin", "String");
            return new Claim1[] { idClaim, dobClaim, roleClaim };
        }
        public static Claim1[] devClaims()
        {
            var ipClaim = new Claim1("IP", "192.168.1.1");
            var devnameClaim = new Claim1("Dev Name", "0811N00032GQXD3");
            var devType = new Claim1("Dev Type", "Laptop");
            return new Claim1[] { ipClaim, devnameClaim, devType };
        }
        public static Claims1Identity UserClaim1Identity()
        {
            var userIdentity = new Claims1Identity();
            userIdentity.Name = "User Identity";
            userIdentity.AuthenticationType = "Bearer";
            userIdentity.Claims = userClaims(); // new Claim1[] {idClaim, dobClaim, roleClaim};
            return userIdentity;
        }
        public static Claims1Identity DevClaim1Identity()
        {
            var devIdentity = new Claims1Identity();
            devIdentity.Name = "Device Identity";
            devIdentity.Claims = devClaims(); ;
            return devIdentity;
        }
        public static Claims1Principal GetUserPrincipal()
        {
            var userPrincipal = new Claims1Principal();
            userPrincipal.Identity = UserClaim1Identity();
            userPrincipal.Claims = userClaims();
            userPrincipal.claims1Identities =  new Claims1Identity[] { UserClaim1Identity() };
            return userPrincipal;

        }
        public static Claims1Principal GetDevPrincipal()
        {
            var devPrincipal = new Claims1Principal();
            devPrincipal.Identity = DevClaim1Identity();
            devPrincipal.Claims = devClaims();
            devPrincipal.claims1Identities = new Claims1Identity[] { DevClaim1Identity() };
            return devPrincipal;

        }
        public static Claims1Principal[] GetPrincipalAll()
        { 
            var allPrincipal = new Claims1Principal[]{ GetDevPrincipal(), GetUserPrincipal() };
 
            return allPrincipal;

            ;
        }

    }
}
