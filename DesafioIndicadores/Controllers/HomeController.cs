using DesafioIndicadores.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NHibernate;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;

namespace DesafioIndicadores.Controllers
{
    public class HomeController : Controller
    {
        ISession _session;
        public HomeController()
        {
            _session = NHibernateHelper.GetCurrentSession();
        }
        public ActionResult Index()
        {
            this._session = NHibernateHelper.GetCurrentSession();
            var tipo = this._session.QueryOver<TipoIndicador>().List<TipoIndicador>().ToList();
            if (tipo.Count == 0)
            {
                this.cargaTipoIndicadorInicial();
            }

            var indicadores = this._session.QueryOver<Indicador>().List<Indicador>().ToList().OrderByDescending(x=>x.fecha).ToList();
            if (indicadores.Count == 0)
            {
                this.cargaIndicadorInicial(tipo);
            }

            var hoy = DateTime.Today.Date;

            if(indicadores.First().fecha.Date < hoy)
            {
                this.cargaDiario(tipo,hoy);
            }
            NHibernateHelper.CloseSession();
            
            return View(tipo);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public List<IndicadorWeb> cargaWeb(List<TipoIndicador>tipo)
        {
            List<IndicadorWeb> retorno = new List<IndicadorWeb>();
            this._session = NHibernateHelper.GetCurrentSession();
            foreach (var item in tipo)
            {
                IndicadorWeb nuevo = new IndicadorWeb();
                nuevo.tipo = item;
                nuevo.indicadores = this._session.QueryOver<Indicador>().Where(x=>x.tipoIndicador.Id == item.Id).List<Indicador>().OrderByDescending(x=>x.fecha).ToList();
                retorno.Add(nuevo);
            }
            NHibernateHelper.CloseSession();
            return retorno;
        }

        public IndicadorWeb getIndicadorByTipo(string filtro)
        {
            IndicadorWeb nuevo = new IndicadorWeb();
            this._session = NHibernateHelper.GetCurrentSession();
            nuevo.tipo = this._session.QueryOver<TipoIndicador>().Where(x=>x.Descripcion==filtro).List<TipoIndicador>().ToList().FirstOrDefault();
            nuevo.indicadores = this._session.QueryOver<Indicador>().Where(x => x.tipoIndicador.Id == nuevo.tipo.Id).List<Indicador>().OrderByDescending(x => x.fecha).ToList();
            NHibernateHelper.CloseSession();
            return nuevo;
        }

        public void cargaDiario(List<TipoIndicador> tipo,DateTime hoy)
        {
            dynamic completo = JsonConvert.DeserializeObject(this.callApi());

            foreach (var item in tipo)
            {
                var paso = completo[item.Descripcion];
                if (((DateTime)paso.fecha).Date == hoy)
                {
                    this.saveIndicador(item, (DateTime)paso.fecha, (double)paso.valor);
                }
            }
        }

        public void cargaTipoIndicadorInicial()
        {
            dynamic completo = JsonConvert.DeserializeObject(this.callApi());

            this.saveTipo(completo["uf"].codigo.Value);
            this.saveTipo(completo["ivp"].codigo.Value);
            this.saveTipo(completo["dolar"].codigo.Value);
            this.saveTipo(completo["dolar_intercambio"].codigo.Value);
            this.saveTipo(completo["euro"].codigo.Value);
            this.saveTipo(completo["ipc"].codigo.Value);
            this.saveTipo(completo["utm"].codigo.Value);
            this.saveTipo(completo["imacec"].codigo.Value);
            this.saveTipo(completo["tpm"].codigo.Value);
            this.saveTipo(completo["libra_cobre"].codigo.Value);
            this.saveTipo(completo["tasa_desempleo"].codigo.Value);
            this.saveTipo(completo["bitcoin"].codigo.Value);

        }
        public void cargaIndicadorInicial(List<TipoIndicador> tipo)
        {
            foreach (var itemTipo in tipo)
            {
                dynamic completo = JsonConvert.DeserializeObject(this.callApi(itemTipo.Descripcion));
                foreach (var item in completo["serie"])
                {
                    saveIndicador(itemTipo, (DateTime)item.fecha.Value, (double)item.valor.Value);
                }
            }

        }

        public void saveTipo(string descripcion)
        {
            try
            {
                this._session = NHibernateHelper.GetCurrentSession();
                // TODO: Add insert logic here
                var tipoIndicador = new TipoIndicador { Descripcion = descripcion };
                this._session.SaveOrUpdate(tipoIndicador);

                //return true;
                NHibernateHelper.CloseSession();
            }
            catch (Exception e)
            {
                var ms = e.Message;
            }
            finally
            {
                NHibernateHelper.CloseSession();
            }
        }
        public void saveIndicador(TipoIndicador idTipo,DateTime fecha,double valor)
        {
            try
            {
                this._session = NHibernateHelper.GetCurrentSession();
                var inserta = this._session.QueryOver<Indicador>().List<Indicador>().Where(x=> x.tipoIndicador.Id == idTipo.Id && x.fecha == fecha).ToList().Count;
                // TODO: Add insert logic here
                if (inserta == 0)
                {
                    var indicador = new Indicador
                    {
                        tipoIndicador = idTipo,
                        fecha = fecha,
                        valor = valor
                    };
                    this._session.SaveOrUpdate(indicador);
                }
                
                //return true;
            }
            catch (Exception e)
            {
                var ms = e.Message;
            }
            finally
            {
                NHibernateHelper.CloseSession();
            }
        }
        
        public string callApi(string urlOptions="")
        {
            string sURL = "https://mindicador.cl/api";
            if (!urlOptions.Equals(""))
            {
                sURL += "/" + urlOptions ;
            }
            
            WebRequest wrGETURL;
            wrGETURL = WebRequest.Create(sURL);

            wrGETURL.Method = "GET";
            wrGETURL.ContentType = @"application/json; charset=utf-8";
                
            HttpWebResponse webresponse = wrGETURL.GetResponse() as HttpWebResponse;

            Encoding enc = System.Text.Encoding.GetEncoding("utf-8");
            // read response stream from response object
            StreamReader loResponseStream = new StreamReader(webresponse.GetResponseStream(), enc);
            // read string from stream data
            string strResult = loResponseStream.ReadToEnd();
            // close the stream object
            loResponseStream.Close();
            // close the response object
            webresponse.Close();

            return strResult;
        }

        [HttpGet]
        public JsonResult GetDataFluctuaDiario()
        {
            this._session = NHibernateHelper.GetCurrentSession();
            var tipo = this._session.QueryOver<TipoIndicador>().List<TipoIndicador>().ToList();
            var indWeb = this.cargaWeb(tipo);
            var uf = indWeb.Where(x => x.tipo.Descripcion == "uf").ToList().FirstOrDefault();
            var ivp = indWeb.Where(x => x.tipo.Descripcion == "ivp").ToList().FirstOrDefault();
            var dolar = indWeb.Where(x => x.tipo.Descripcion == "dolar").ToList().FirstOrDefault();
            var euro = indWeb.Where(x => x.tipo.Descripcion == "euro").ToList().FirstOrDefault();
            var tpm = indWeb.Where(x => x.tipo.Descripcion == "tpm").ToList().FirstOrDefault();
            var libra_cobre = indWeb.Where(x => x.tipo.Descripcion == "libra_cobre").ToList().FirstOrDefault();

            List<FluctuacionDiaria> resultado = new System.Collections.Generic.List<FluctuacionDiaria>();

            for (int i = 0; i < uf.indicadores.Count; i++)
            {
                FluctuacionDiaria nuevo = new FluctuacionDiaria();
                nuevo.Fecha = uf.indicadores[i].fecha.ToString().Split(' ')[0];
                nuevo.Uf = uf.indicadores[i].valor.ToString();
                nuevo.Ivp = ivp.indicadores[i].valor.ToString();
                nuevo.Dolar = dolar.indicadores[i].valor.ToString();
                nuevo.Euro = euro.indicadores[i].valor.ToString();
                nuevo.Tpm = tpm.indicadores[i].valor.ToString();
                nuevo.Libra_cobre = libra_cobre.indicadores[i].valor.ToString();
                resultado.Add(nuevo);
            }
            NHibernateHelper.CloseSession();
            return Json(resultado, "application/json", Encoding.UTF8, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetDataByTipo(string filtro)
        {
            this._session = NHibernateHelper.GetCurrentSession();
            var paso= getIndicadorByTipo(filtro);
            List<DataGraficoUnico> resultado = new List<DataGraficoUnico>();
            foreach (var item in paso.indicadores)
            {
                DataGraficoUnico nuevo = new DataGraficoUnico();
                nuevo.Id = item.Id;
                nuevo.Fecha = item.fecha.ToString().Split(' ')[0];
                nuevo.Valor = item.valor.ToString();
                nuevo.Descripcion = filtro;
                resultado.Add(nuevo);
            }

            NHibernateHelper.CloseSession();
            
            return Json(resultado, "application/json", Encoding.UTF8, JsonRequestBehavior.AllowGet);
        }
    }
}