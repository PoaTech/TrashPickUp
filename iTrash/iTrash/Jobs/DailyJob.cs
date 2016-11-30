using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using iTrash.Models;
using Quartz;

namespace iTrash.Jobs
{
    public class DailyJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            RunDailyTasks();
        }
        public void RunDailyTasks()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            ReturnUsersFromSuspension(db);
            ClearPickupTable(db);
            PopulatePickupTable(db);
            SetTruckRoute(GetZipcodes(db), db);
        }
        public void ReturnUsersFromSuspension(ApplicationDbContext db)
        {

            DateTime date = DateTime.Today;
            var query = (from a in db.Users
                         select a).ToList();
            foreach (ApplicationUser user in query)
            {
                if (GetDate(user._ReturnDate_ID, db) == date)
                {
                    user._ReturnDate_ID = null;
                    user._LeaveDate_ID = null;
                }
            }
        }
        public void ClearPickupTable(ApplicationDbContext db)
        {
            var pickups = db.Pickup.ToList();
            db.Pickup.RemoveRange(pickups);
            db.SaveChanges();
        }
        public void PopulatePickupTable(ApplicationDbContext db)
        {
            List<ApplicationUser> users = GetUserList(db);
            foreach (ApplicationUser user in users)
            {
                Pickup newPickup = new Pickup();
                newPickup._User = user.Id;
                db.Pickup.Add(newPickup);
                db.SaveChanges();
            }
        }
        public List<ApplicationUser> GetUserList(ApplicationDbContext db)
        {
            List<ApplicationUser> usersNeedingPickup = new List<ApplicationUser>();
            DateTime date = DateTime.Today;
            string currentDay = date.DayOfWeek.ToString();
            var query = (from a in db.Users
                     where a.pickupDay._Day == currentDay && a._AltPickupDay_ID == null
                         select a).ToList();
            foreach (ApplicationUser user in query)
            {
                if (date < GetDate(user._LeaveDate_ID, db))
                {
                    usersNeedingPickup.Add(user);
                }
            }
            query = (from a in db.Users
                         where a.altPickupDay._Day == currentDay
                         select a).ToList();
            foreach (ApplicationUser user in query)
            {
                if (date < GetDate(user._LeaveDate_ID, db))
                {
                    usersNeedingPickup.Add(user);
                    user._AltPickupDay_ID = null;
                }
            }
            return usersNeedingPickup;
        }
        public DateTime GetDate(int? dateId, ApplicationDbContext db)
        {
            try
            {
                var queryDay = (from a in db.CalendarDay
                                where a._ID == ((from b in db.Date
                                                 where b._ID == dateId
                                                 select new { b._Day }).FirstOrDefault()._Day)
                                select new { a._Day }).First();

                var queryMonth = (from a in db.Month
                                  where a._ID == ((from b in db.Date
                                                   where b._ID == dateId
                                                   select new { b._Month }).FirstOrDefault()._Month)
                                  select new { a._Month }).First();

                var queryYear = (from a in db.Year
                                 where a._ID == ((from b in db.Date
                                                  where b._ID == dateId
                                                  select new { b._Year }).FirstOrDefault()._Year)
                                 select new { a._Year }).First();

                int day = queryDay._Day;
                int month = queryMonth._Month;
                int year = queryYear._Year;

                return new DateTime(year, month, day);
            }
            catch
            {
                return DateTime.Today.AddDays(1);
            }
        }
        public void SetTruckRoute(List<int> pickupZipcodes, ApplicationDbContext db)
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
                    truck._Zipcode = pickupZipcodes[i];
                }
                i++;
            }
            for (int j = i; j < pickupZipcodes.Count; j++)
            {
                Truck newTruck = new Truck();
                newTruck._TruckNumber = newTruck._ID + "";
                newTruck._Zipcode = pickupZipcodes[j];
                db.Truck.Add(newTruck);
            }
            db.SaveChanges();
        }
        public List<int> GetZipcodes(ApplicationDbContext db)
        {
            List<int> addressIds = new List<int>();
            List<int> zipcodeIds = new List<int>();
            var userids = (from users in db.Pickup
                           select users._User).ToList();
            foreach (string userid in userids)
            {
                var addressId = (from user in db.Users
                                 where user.Id == userid
                                 select user._Address_ID).First();
                if (addressId != null)
                {
                    addressIds.Add(addressId.Value);
                }
            }
            foreach (int? addressId in addressIds)
            {
                var zipcodeId = (from address in db.Address
                                  where address._ID == addressId
                                  select address).First();
                zipcodeIds.Add(zipcodeId._Zipcode);
            }
            return zipcodeIds.Distinct().ToList();
        }
    }
}