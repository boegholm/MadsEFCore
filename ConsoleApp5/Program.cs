
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace KeramikTracker.Shared.DatabaseModels;
using static OrderState;
public class Program
{

    public static void Main(string[] args)
    {

        string[] n = { "Alma", "Agnes", "Ella", "Freja", "Clara", "Emma", "Sofia", "Karla", "Anna", "Ellie", "Olivia", "Alberte", "Nora", "Asta", "Laura", "Alfred", "Oscar", "Carl", "Noah", "William", "Oliver", "Aksel", "Arthur", "Valdemar", "Lucas", "Malthe", "Emil", "August", "Victor", "Elias" };
        using (var db = new ABMDbContext())
        {
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            List<Order> orders = Enumerable.Range(0, 10).Select(v => new Order { Guests=n.Skip(v*2).Take(2).Select(v=>new Guest { Name = v }).ToList(), State = (OrderState)(v % Enum.GetNames(typeof(OrderState)).Length) }).ToList();
            List<Table> tables = Enumerable.Range(0, 10).Select(v => new Table { Name = $"Table {v}", Orders = orders.Take(v).ToList() }).ToList();
            db.AddRange(orders);
            db.AddRange(tables);
            db.SaveChanges();
        }
        int guestid = 2;
        OrderState s = Active;
        using (var db = new ABMDbContext(LoggerFactory.Create(b =>
        {
            b.AddConsole();
        })))
        {
            //var ex1 = db.Orders
            //.Include(v => v.Guests)
            //.Include(v => v.Tables)
            //.Where(v => v.State == s && v.Guests.Any(v => v.Id == guestid)).ToList();

            var k = db.Tables.Where(v => v.Id == 4).ToList();
            
            var g = db.Guests.Where(v=>v.Orders.Any(v=>v.Id == 4)).ToList();
            var ss = db.Guests.Include(v => v.Orders).ThenInclude(v => v.Tables).Where(v => v.Orders.Any(v => v.Id == 4)).ToList();
            var asdf = new Table() { Id = 1 };

            db.Entry(asdf).Property<int>("id");

            var ex2 = db.Orders
                .Include(v => v.Guests)

                .Include(v => v.Tables)
                .Where(v => v.State == s && v.Guests.Any(v => v.Id == guestid))
                .Select(v => new
                {
                    v.State,
                    Tables = v.Tables.Select(v => new
                    {
                        v.Id,
                        v.Name
                    }),
                }).ToList();

            //var gs = ex2.Join(db.Tables, v => v.Tables.Select(t => t.Id), v => v.Id, (a, b) => new { a, b });
            
                

        }
    }
}
