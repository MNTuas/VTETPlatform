using VTET.Business;
using VTET.Data.Models;

Console.WriteLine("Hello, World!");

var orderDetailBusiness = new OrderDetailBusiness();

// Get All OrderDetails
Console.WriteLine("OrderDetailBusiness.GetAll()");
var orderdetailResult = await orderDetailBusiness.GetAll();

if (orderdetailResult.Status > 0 && orderdetailResult.Data != null)
{
    var orderdetails = (List<OrderDetail>)orderdetailResult.Data;

    if (orderdetails != null && orderdetails.Count > 0)
    {
        foreach (var orderdetail in orderdetails)
        {
            Console.WriteLine($"OrderDetailID: {orderdetail.Id}, WatchID: {orderdetail.WatchId}, OrderID: {orderdetail.OrderId}, Price: {orderdetail.Price}, Amount: {orderdetail.Amount}");
        }
    }
}
else
{
    Console.WriteLine("Get All OrderDetail failed");
}

// Delete
/*Console.WriteLine("OrderDetailBusiness.DeleteOrderDetail()");
int deleteOrderDetailId = 4;
var deleteResult = await orderDetailBusiness.Delete(deleteOrderDetailId);
Console.WriteLine($"Delete: {deleteResult.Message}"); */