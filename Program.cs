using Northwind_Hw;
using Northwind_Hw.Modelss;
using System;
using System.Linq;

var db = new NorthWindContext();
//var catogries= db.Categories.OrderBy(c => c.CategoryName).ToList();
//foreach(var item in catogries)
//{
//    Console.WriteLine(item.CategoryName);
//}
var region = db.Regions.ToList();   
//foreach(var item in region)
//{
//    Console.WriteLine(item.RegionDescription);
//}
var tr=db.Territories.ToList();
//foreach (var item in tr)
//{
//    Console.WriteLine(item.TerritoryDescription);
//}
var query =
     region.GroupJoin(tr,
                      region => region.RegionId,
                      tr=> tr.RegionId,
                      (Region, trCollection) =>
                          new
                          {
                              regionName = Region.RegionDescription,
                              terrs = trCollection.Select(ter => ter.TerritoryDescription)
                          });

foreach (var obj in query)
{
    
    Console.WriteLine("{0}:", obj.regionName);
   
    foreach (var terr in obj.terrs)
    {
        Console.WriteLine("  {0}", terr);
    }
}
DateTime aDate = DateTime.Now;
var orders = db.Orders.ToList();
var newOrder = new Order
{
    CustomerId = "FRANK",
    EmployeeId = 6,
    OrderDate = aDate,
    ShipAddress = "7 Piccadilly Road.",
    ShipCity = "New York",
    ShipCountry = "New York",
};

db.Orders.Add(newOrder);
db.SaveChanges();
var Ordet1 = new OrderDetail { ProductId = 11, UnitPrice = 95, Quantity = 3, OrderId = newOrder.OrderId };
var Ordet2 = new OrderDetail { ProductId = 56, UnitPrice = 47, Quantity = 6, OrderId = newOrder.OrderId };
var Ordet3 = new OrderDetail { ProductId = 74, UnitPrice = 120, Quantity = 5, OrderId = newOrder.OrderId };
db.OrderDetails.Add(Ordet1);
db.OrderDetails.Add(Ordet2);
db.OrderDetails.Add(Ordet3);
db.SaveChanges();
var proudcutNameAndEmploy = db.Employees.Join(db.Orders, emp => emp.EmployeeId, order => order.EmployeeId,
    (emp, order) => new { emp.EmployeeId, emp.FirstName, emp.LastName, order }).Join(db.OrderDetails,
    emp => emp.order.OrderId, OrderDetail => OrderDetail.OrderId, (emp, OrderDetail) => new
    { emp, emp.order, OrderDetail }).Join(db.Products, emp => emp.OrderDetail.ProductId, pro => pro.ProductId, (emp, pro) =>
               new { emp, emp.order, emp.OrderDetail, pro }).ToList();
proudcutNameAndEmploy.ForEach(emp => Console.WriteLine($"The name of the product is{emp.pro.ProductName}"+'\n'+ $"The name of the employee is{emp.emp.emp.FirstName}"+'\n')); 
                          
var orderDett= db.Orders.ToList();
foreach(var order in orderDett)
{
    if (order.OrderId == 11079)
    {
        order.EmployeeId = 5;
    }
        
    
}
db.SaveChanges();
var orderDett1 = db.OrderDetails.ToList();
foreach (var order in orderDett1)
{
    if (order.OrderId == 11079 && order.ProductId == 56)
    {
        db.OrderDetails.Remove(order);
    }

}
db.SaveChanges();