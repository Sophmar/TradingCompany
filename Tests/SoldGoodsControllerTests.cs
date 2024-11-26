using AutoMapper;
using BLL.Interfaces;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TradingCompanyApp.Controllers;
using TradingCompanyApp.Models;

namespace Tests
{
    public class SoldGoodsControllerTests
    {/*
        private readonly Mock<ISoldGoodsServices> _mockSoldGoodsServices;
        private readonly Mock<IUserServices> _mockUserServices;
        private readonly Mock<IMapper> _mockMapper;
        private readonly SoldGoodsController _controller;

        public SoldGoodsControllerTests()
        {
            _mockSoldGoodsServices = new Mock<ISoldGoodsServices>();
            _mockUserServices = new Mock<IUserServices>();
            _mockMapper = new Mock<IMapper>();

            _controller = new SoldGoodsController(
                _mockSoldGoodsServices.Object,
                _mockUserServices.Object,
                _mockMapper.Object
            );
        }

        [Test]
        public void Index_ReturnsViewResult_WithListOfSoldGoods()
        {
            var mockSoldGoodsList = new List<SoldGoods>
        {
            new SoldGoods { Sell_Id = 1, Goods_Name = "Product A", Cost_Get = 100, Amount_Lost = 2, Date = System.DateTime.Now },
            new SoldGoods { Sell_Id = 2, Goods_Name = "Product B", Cost_Get = 150, Amount_Lost = 3, Date = System.DateTime.Now }
        };

            var mockModelsList = new List<SoldGoodsDetailsModel>
        {
            new SoldGoodsDetailsModel { Sell_Id = 1, Goods_Name = "Product A", Cost_Get = 100, Amount_Lost = 2 },
            new SoldGoodsDetailsModel { Sell_Id = 2, Goods_Name = "Product B", Cost_Get = 150, Amount_Lost = 3 }
        };

            _mockSoldGoodsServices.Setup(service => service.GetAll()).Returns(mockSoldGoodsList);
            _mockMapper.Setup(mapper => mapper.Map<List<SoldGoodsDetailsModel>>(mockSoldGoodsList)).Returns(mockModelsList);

            var result = _controller.Index();

            var viewResult = Assert.IsInstanceOf<ViewResult>(result);
            var model = Assert.IsInstanceOf<List<SoldGoodsDetailsModel>>(viewResult.Model);
            Assert.Equals(2, model.Count());
        }

        [Test]
        public void Create_PostValidModel_ReturnsRedirectToAction()
        {
            var model = new EditSoldGoodsModel { Goods_Name = "Product C", Cost_Get = 200, Amount_Lost = 4 };
            var sell = new SoldGoods { Goods_Name = "Product C", Cost_Get = 200, Amount_Lost = 4 };

            _mockMapper.Setup(mapper => mapper.Map<SoldGoods>(model)).Returns(sell);

            var result = _controller.Create(model);

            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equals("Index", redirectResult.ActionName);
            _mockSoldGoodsServices.Verify(service => service.Add(sell), Times.Once);
        }

        [Test]
        public void Edit_GetValidId_ReturnsViewWithModel()
        {
            int validId = 1;
            var mockSoldGoods = new SoldGoods { Sell_Id = validId, Goods_Name = "Product A", Cost_Get = 100, Amount_Lost = 2, Date = System.DateTime.Now };
            var mockEditModel = new EditSoldGoodsModel { Sell_Id = validId, Goods_Name = "Product A", Cost_Get = 100, Amount_Lost = 2 };

            _mockSoldGoodsServices.Setup(service => service.GetById(validId)).Returns(mockSoldGoods);
            _mockMapper.Setup(mapper => mapper.Map<EditSoldGoodsModel>(mockSoldGoods)).Returns(mockEditModel);

            var result = _controller.Edit(validId);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<EditSoldGoodsModel>(viewResult.Model);
            Assert.Equals(validId, model.Sell_Id);
        }

        [Test]
        public void Delete_PostValidId_ReturnsRedirectToAction()
        {
            int validId = 1;

            var result = _controller.Delete(validId, new EditSoldGoodsModel());

            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equals("Index", redirectResult.ActionName);
            _mockSoldGoodsServices.Verify(service => service.DeleteById(validId), Times.Once);
        }*/
    }

}