using ProductWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ProductWebApi.Controllers
{
    public class ProductsController : ApiController
    {
        static List<Product> _productList = null;
        void Initialize()
        {
            _productList = new List<Product>()
           {
               new Product() { productId=1, Name="Mobile" , QtyInStock=3, Description="Used for Communication", Supplier="Madhuri"},

               new Product() { productId=2, Name="PenDrive" , QtyInStock=4, Description="Used for store data", Supplier="Prashanthi"},
           };
        }
        public ProductsController()
        {
            if (_productList == null)
            {
                Initialize();
            }
        }
        public HttpResponseMessage Get()
        {
            return Request.CreateResponse(HttpStatusCode.OK, _productList);
        }

        public HttpResponseMessage Get(int id)
        {
            Product product = _productList.FirstOrDefault(x => x.productId == id);
            if (product == null)
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Product Not found");
            else
                return Request.CreateResponse(HttpStatusCode.OK, product);
        }

        public HttpResponseMessage Post(Product product)
        {
            if (product != null)
            {
                _productList.Add(product);
            }
            return Request.CreateResponse(HttpStatusCode.Created);
        }

        public HttpResponseMessage Put(int id, Product objProduct)
        {
            Product product = _productList.Where(x => x.productId == id).FirstOrDefault();
            if (product == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Product Not found");

            }

            else
            {
                if (product != null)
                {
                    foreach (Product temp in _productList)
                    {
                        if (temp.productId == id)
                        {
                            temp.Name = objProduct.Name;
                            temp.QtyInStock = objProduct.QtyInStock;
                            temp.Description = objProduct.Description;
                            temp.Supplier = objProduct.Supplier;
                        }
                    }


                }
                return Request.CreateResponse(HttpStatusCode.OK, "Modified");

            }
        }
        public HttpResponseMessage Delete(int id)
        {
            Product product = _productList.Where(x => x.productId == id).FirstOrDefault();
            if (product == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Product Not found");

            }
            else
            {
                if (product != null)
                {
                    _productList.Remove(product);
                }
                return Request.CreateResponse(HttpStatusCode.OK, "Deleted");
            }

        }
    }
}
