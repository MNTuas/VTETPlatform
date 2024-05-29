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
            Console.WriteLine($"OrderDetailID: {orderdetail.OrderDetailId}, WatchID: {orderdetail.WatchId}, OrderID: {orderdetail.OrderId}, Price: {orderdetail.Price}, Amount: {orderdetail.Amount}");
        }
    }
}
else
{
    Console.WriteLine("Get All OrderDetail failed");

    /*// Insert
    Console.WriteLine("OrderDetailBusiness.InsertOrderDetail()");
    var newOrderDetail = new OrderDetail
    {
        WatchId = 1,
        OrderId = 1,
        Price = 100,
        Amount = 2
    };
    var insertResult = await orderDetailBusiness.Save(newOrderDetail);
    Console.WriteLine($"Insert: {insertResult.Message}");


    // GetById
    Console.WriteLine("OrderDetailBusiness.GetOrderDetailById()");
    int orderDetailId = newOrderDetail.OrderDetailId;
    var getByIdResult = await orderDetailBusiness.GetById(orderDetailId);
    if (getByIdResult.Status > 0 && getByIdResult.Data != null)
    {
        var orderDetail = (OrderDetail)getByIdResult.Data;
        Console.WriteLine($"OrderDetailID: {orderDetail.OrderDetailId}, WatchID: {orderDetail.WatchId}, OrderID: {orderDetail.OrderId}, Price: {orderDetail.Price}, Amount: {orderDetail.Amount}");
    }
    else
    {
        Console.WriteLine($"GetById failed: {getByIdResult.Message}");
    }


    //Update
    Console.WriteLine("OrderDetailBusiness.UpdateOrderDetail()");
    var updateOrderDetail = new OrderDetail
    {
        OrderDetailId = orderDetailId,
        WatchId = 2,
        OrderId = 1,
        Price = 150,
        Amount = 3
    };
    var updateResult = await orderDetailBusiness.Update(updateOrderDetail);
    Console.WriteLine($"Update: {updateResult.Message}");

    // Delete
    Console.WriteLine("OrderDetailBusiness.DeleteOrderDetail()");
    int deleteOrderDetailId = orderDetailId;
    var deleteResult = await orderDetailBusiness.Delete(deleteOrderDetailId);
    Console.WriteLine($"Delete: {deleteResult.Message}");*/
}
