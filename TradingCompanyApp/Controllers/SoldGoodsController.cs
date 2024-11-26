using AutoMapper;
using BLL.Interfaces;
using DAL;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using TradingCompanyApp.Models;

namespace TradingCompanyApp.Controllers
{
    public class SoldGoodsController : Controller
    {
        protected readonly ISoldGoodsServices _sellServices;
        protected readonly IUserServices _userServices;
        protected readonly IMapper _mapper;

        public SoldGoodsController(ISoldGoodsServices sellServices,
            IUserServices userServices, IMapper mapper)
        {
            _sellServices = sellServices;
            _userServices = userServices;
            _mapper = mapper;
        }


        // GET: SoldGoodsController
        public ActionResult Index()
        {
            var soldGoods = _mapper.Map<List<SoldGoodsDetailsModel>>(_sellServices.GetAll());
            return View(soldGoods);
        }

        // GET: SoldGoodsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SoldGoodsController/Create
        public ActionResult Create()
        {
            var model = new EditSoldGoodsModel();
            
            return View(model);
        }

        // POST: SoldGoodsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EditSoldGoodsModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var sell = _mapper.Map<SoldGoods>(model);
                    _sellServices.Add(sell);

                    return RedirectToAction(nameof(Index));
                }
                return View(model);
            }
            catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(model);
            }
        }

        // GET: SoldGoodsController/Edit/5
        public ActionResult Edit(int id)
        {
            var soldGoods = _sellServices.GetById(id);

            if (soldGoods == null)
            {
                return NotFound();
            }

            var model = _mapper.Map<EditSoldGoodsModel>(soldGoods);

            return View(model);
        }

        // POST: SoldGoodsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, EditSoldGoodsModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var updatedSell = _mapper.Map<SoldGoods>(model);
                    updatedSell.Sell_Id = id; 

                    _sellServices.Update(updatedSell, id);

                    return RedirectToAction(nameof(Index));
                }
                return View(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(model);
            }
        }

        // GET: SoldGoodsController/Delete/5
        public ActionResult Delete(int id)
        {

            var soldGoods = _sellServices.GetById(id);

            if (soldGoods == null)
            {
                return NotFound();
            }

            var model = _mapper.Map<EditSoldGoodsModel>(soldGoods);

            return View(model);
        }

        // POST: SoldGoodsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, EditSoldGoodsModel model)
        {
            try
            {
                _sellServices.DeleteById(id);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return RedirectToAction(nameof(Delete), new { id });
            }
        }
    }
}
