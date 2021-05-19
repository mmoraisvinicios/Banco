using Banco.Controllers;
using Banco.Models;
using Bogus;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace TesteUnitario
{
    public class ControllerTest
    {
        private ContasController _controller;
        private Mock<IBaseServico<Conta>> _mockBaseServico;
        [SetUp]
        public void Setup()
        {
            _mockBaseServico = new Mock<IBaseServico<Conta>>();
            var mockBanco = new Mock<IBancos>(); 
            _controller = new ContasController(_mockBaseServico.Object, mockBanco.Object);
        
        }

        [Test]
        public void Controller_Teste_Ok()
        {
            var contas = new Faker<ListarContas>("pt_BR")
                .Generate(10);

            _mockBaseServico.Setup(x => x.Buscar<ListarContas>()).Returns(contas);

            var resultado = _controller.Buscar() as OkObjectResult;
            var obj = resultado.Value as List<ListarContas>;

            Assert.AreEqual(200,resultado.StatusCode);
            Assert.AreEqual(10, obj.Count);

            Assert.Pass();
        }
    }
}