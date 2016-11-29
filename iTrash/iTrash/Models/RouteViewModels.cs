using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace iTrash.Models
{
    public class RouteViewModel
    {
        private ApplicationDbContext db;

        public RouteViewModel(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void AssignPickups()
        {
            ClearPickups();
            SetPickups(GetUserList(false));
            AssignTrucks(GetZipcodes());
        }
        public void ClearPickups()
        {
            string sqlTrunc = "TRUNCATE TABLE Pickups";
            SqlCommand cmd = new SqlCommand(sqlTrunc);
            cmd.ExecuteNonQuery();
        }
        public List<ApplicationUser> GetUserList(bool pickupSuspended)
        {
            List<ApplicationUser> usersNeedingPickup = new List<ApplicationUser>();
            DateTime date = new DateTime();
            string currentDay = date.DayOfWeek.ToString();
            var query = (from a in db.Users
                         where a.altPickupDay._Day == currentDay && !pickupSuspended
                         select a);
            foreach (ApplicationUser user in query)
            {
                usersNeedingPickup.Add(user);
                user._AltPickupDay_ID = null;
            }
            query = (from a in db.Users
                         where a.pickupDay._Day == currentDay && a._AltPickupDay_ID == null && !pickupSuspended
                     select a);
            foreach (ApplicationUser user in query)
            {
                usersNeedingPickup.Add(user);
            }
            return usersNeedingPickup;
        }
        public void SetPickups(List<ApplicationUser> users)
        {
            foreach (ApplicationUser user in users)
            {
                Pickup newPickup = new Pickup();
                newPickup._User = user.Id;
                db.Pickup.Add(newPickup);
                db.SaveChanges();
            }
        }
        public List<Zipcode> GetZipcodes()
        {
            List<Zipcode> pickupZipcodes = new List<Zipcode>();
            foreach (Pickup pickup in db.Pickup)
            {
                pickupZipcodes.Add(pickup.user.address.zipcode);
            }
            return pickupZipcodes.Distinct().ToList();
        }
        public void AssignTrucks(List<Zipcode> pickupZipcodes)
        {
            int i = 0;
            foreach (Truck truck in db.Truck)
            {
                if (i > pickupZipcodes.Count)
                {
                    truck._Zipcode = null;
                }
                else
                {
                    truck._Zipcode = pickupZipcodes[i]._ID;
                }
                i++;
            }
            for (int j = i; j < pickupZipcodes.Count; j++)
            {
                Truck newTruck = new Truck();
                newTruck._Zipcode = pickupZipcodes[j]._ID;
                db.Truck.Add(newTruck);
            }
            db.SaveChanges();
        }
    }
}