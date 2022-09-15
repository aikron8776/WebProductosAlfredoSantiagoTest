using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebProductos.Models;
using System.Configuration;
using System.Net.Http.Formatting;
using System.Web.UI.WebControls;
using Microsoft.Ajax.Utilities;

namespace WebProductos.Controllers
{
    public class ProductosController : Controller
    {
        public Producto ObjProductos = new Producto()
        {
            Id = 1,
            nombre = "Teclado",
            cantidad = 5,
            categoria = ""
        };

        string Baseurl = ConfigurationManager.AppSettings["URLWS"].ToString();
        public async Task<ActionResult> Index(string Search_Data)
        {
            List<Producto> lstProducto = new List<Producto>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/Producto/ListarProductos");
                if (Res.IsSuccessStatusCode)
                {
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    lstProducto = JsonConvert.DeserializeObject<List<Producto>>(EmpResponse);
                }

                if (Search_Data != null)
                {
                    lstProducto = (from l in lstProducto
                                   where l.nombre.ToUpper().Contains(Search_Data.ToUpper()) ||
                                    l.categoria.ToUpper().Contains(Search_Data.ToUpper()) ||
                                    l.descripcion.ToUpper().Contains(Search_Data.ToUpper())

                                   select l).ToList();
                }


                return View(lstProducto);
            }
        }

        // GET: Productos
        public ActionResult Index_BKP()
        {


            return View();
        }

        // GET: Productos/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Productos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Productos/Create
        [HttpPost]
        public async Task<ActionResult> Create(Producto collection)
        {
            try
            {
                List<Producto> lstProducto = new List<Producto>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var myContent = JsonConvert.SerializeObject(collection);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");


                    HttpResponseMessage Res = await client.PostAsync("api/Producto/Post", byteContent);
                    if (Res.IsSuccessStatusCode)
                    {
                        var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    }
                }


                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        // GET: Productos/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            List<Producto> lstProducto = new List<Producto>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/Producto/ListarProductos");
                if (Res.IsSuccessStatusCode)
                {
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    lstProducto = JsonConvert.DeserializeObject<List<Producto>>(EmpResponse);
                }

                return View(lstProducto.Where(x => x.Id == id).FirstOrDefault());
            }
        }

        // POST: Productos/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int id, Producto collection)
        {
            try
            {
                // TODO: Add update logic here

                List<Producto> lstProducto = new List<Producto>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var myContent = JsonConvert.SerializeObject(collection);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");


                    HttpResponseMessage Res = await client.PutAsync("api/Producto/Put", byteContent);
                    if (Res.IsSuccessStatusCode)
                    {
                        var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    }
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Productos/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            List<Producto> lstProducto = new List<Producto>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/Producto/ListarProductos");
                if (Res.IsSuccessStatusCode)
                {
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    lstProducto = JsonConvert.DeserializeObject<List<Producto>>(EmpResponse);
                }

                return View(lstProducto.Where(x => x.Id == id).FirstOrDefault());
            }
        }

        // POST: Productos/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(int id, Producto collection)
        {
            try
            {
                // TODO: Add delete logic here

                List<Producto> lstProducto = new List<Producto>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage Res = await client.DeleteAsync("api/Producto/Delete/" + id);
                    if (Res.IsSuccessStatusCode)
                    {
                        var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    }
                }


                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
