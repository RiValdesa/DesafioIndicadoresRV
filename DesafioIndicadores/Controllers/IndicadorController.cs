using DesafioIndicadores.Models;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace DesafioIndicadores.Controllers
{
    public class IndicadorController : Controller
    {
        ISession _session;
        public IndicadorController()
        {
            _session = NHibernateHelper.GetCurrentSession();
        }
        // GET: Indicador
        public ActionResult Index()
        {
            return View();
        }

        public IndicadorWeb getIndicadorByTipo(int id)
        {
            this._session = NHibernateHelper.GetCurrentSession();
            IndicadorWeb nuevo = new IndicadorWeb();
            nuevo.tipo = this._session.QueryOver<TipoIndicador>().Where(x => x.Id == id).List<TipoIndicador>().ToList().FirstOrDefault();
            nuevo.indicadores = this._session.QueryOver<Indicador>().Where(x => x.tipoIndicador.Id == id).List<Indicador>().OrderByDescending(x => x.fecha).ToList();
            NHibernateHelper.CloseSession();
            return nuevo;
        }
        // GET: Indicador/Details/5
        public ActionResult Details(int id)
        {
            IndicadorWeb ret = getIndicadorByTipo(id);
            return View(ret);
        }

        [HttpPost]
        public bool saveIndicador(int id, double valor)
        {
            bool retorno = false;
            try
            {
                // TODO: Add insert logic here
                this._session = NHibernateHelper.GetCurrentSession();
                var indicador = this._session.Get<Indicador>(id);

                indicador.valor = valor;

                this._session.SaveOrUpdate(indicador);
                this._session.Flush();
                retorno = true;
            }
            catch (Exception e)
            {
                var ms = e.Message;
                retorno = false;
            }
            finally
            {
                NHibernateHelper.CloseSession();
            }

            return retorno;
        }

        // GET: Indicador/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Indicador/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Indicador/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Indicador/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Indicador/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Indicador/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
