using NUnit.Framework;
using Moq;
using System;
using System.Collections.Generic;
using BLL.Concrete;
using DAL;
using DTO;

namespace Tests
{
    [TestFixture]
    public class SoldGoodsServicesTests
    {
        private Mock<ISoldGoodsDAL> _mockSellDal;
        private SoldGoodsServices _sellGoodsService;

        [SetUp]
        public void SetUp()
        {
            _mockSellDal = new Mock<ISoldGoodsDAL>();
            _sellGoodsService = new SoldGoodsServices(_mockSellDal.Object);
        }

        [Test]
        public void GetAll_ReturnsListOfSoldGoods()
        {
            var expectedSoldGoods = new List<SoldGoods>
            {
                new SoldGoods { Sell_Id = 1, Goods_Name = "A" },
                new SoldGoods { Sell_Id = 2, Goods_Name = "B" }
            };
            _mockSellDal.Setup(dal => dal.GetAll()).Returns(expectedSoldGoods);

            var result = _sellGoodsService.GetAll();

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
        }

        [Test]
        public void Add_CallsDalAdd_AndReturnsCorrectId()
        {
            var newSoldGoods = new SoldGoods { Sell_Id = 1, Goods_Name = "New Product" };
            _mockSellDal.Setup(dal => dal.Add(newSoldGoods)).Returns(1);

            var result = _sellGoodsService.Add(newSoldGoods);

            Assert.AreEqual(1, result);
            _mockSellDal.Verify(dal => dal.Add(newSoldGoods), Times.Once);
        }

        [Test]
        public void GetById_ReturnsSoldGoods_WhenSoldGoodsExists()
        {
            var expectedSoldGoods = new SoldGoods { Sell_Id = 1, Goods_Name = "Existing Product" };
            _mockSellDal.Setup(dal => dal.GetById(1)).Returns(expectedSoldGoods);

            var result = _sellGoodsService.GetById(1);

            Assert.IsNotNull(result);
            Assert.AreEqual(expectedSoldGoods, result);
            _mockSellDal.Verify(dal => dal.GetById(1), Times.Once);
        }

        [Test]
        public void GetById_ReturnsNull_WhenSoldGoodsDoesNotExist()
        {
            _mockSellDal.Setup(dal => dal.GetById(999)).Returns((SoldGoods)null);

            var result = _sellGoodsService.GetById(999);

            Assert.IsNull(result);
            _mockSellDal.Verify(dal => dal.GetById(999), Times.Once);
        }

        [Test]
        public void Update_CallsDalUpdate_AndReturnsCorrectValue()
        {
            var updatedSoldGoods = new SoldGoods { Sell_Id = 1, Goods_Name = "Updated Product" };
            _mockSellDal.Setup(dal => dal.Update(updatedSoldGoods, 1)).Returns(true);

            var result = _sellGoodsService.Update(updatedSoldGoods, 1);

            Assert.IsTrue(result);
            _mockSellDal.Verify(dal => dal.Update(updatedSoldGoods, 1), Times.Once);
        }

        [Test]
        public void DeleteById_CallsDalDeleteById_AndReturnsCorrectValue()
        {
            _mockSellDal.Setup(dal => dal.DeleteById(1)).Returns(true);

            var result = _sellGoodsService.DeleteById(1);

            Assert.IsTrue(result);
            _mockSellDal.Verify(dal => dal.DeleteById(1), Times.Once);
        }
    }
}
