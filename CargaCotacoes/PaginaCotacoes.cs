using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace CargaCotacoes
{
    public class PaginaCotacoes
    {
        private SeleniumConfigurations _configurations;
        private IWebDriver _driver;

        public PaginaCotacoes(SeleniumConfigurations seleniumConfigurations)
        {
            _configurations = seleniumConfigurations;

            ChromeOptions optionsFF = new ChromeOptions();
            optionsFF.AddArgument("--headless");

            _driver = new ChromeDriver(
                _configurations.CaminhoDriverChrome
                , optionsFF);
        }

        public void CarregarPagina()
        {
            _driver.Manage().Timeouts().PageLoad =
                TimeSpan.FromSeconds(60);
            _driver.Navigate().GoToUrl(
                _configurations.UrlPaginaCotacoes);
        }

        public List<CotacaoMoedaEstrangeira> ObterCotacoesMoedasEstrangeiras()
        {
            var cotacoes = new List<CotacaoMoedaEstrangeira>();

            var rowsCotacoes = _driver
                .FindElement(By.Id("tableCotacoes"))
                .FindElement(By.TagName("tbody"))
                .FindElements(By.TagName("tr"));
            foreach (var rowCotacao in rowsCotacoes)
            {
                var dadosCotacao = rowCotacao.FindElements(
                    By.TagName("td"));

                var cotacao = new CotacaoMoedaEstrangeira();
                cotacao.Codigo = dadosCotacao[0].Text;
                cotacao.NomeMoeda = dadosCotacao[1].Text;
                cotacao.Variacao = dadosCotacao[2].Text;
                cotacao.ValorReais = Convert.ToDouble(
                    dadosCotacao[3].Text);

                cotacoes.Add(cotacao);
            }

            return cotacoes;
        }

        public CotacaoBitcoin ObterCotacaoBitcoin()
        {
            CotacaoBitcoin cotacao = null;

            var cotacaoBitcoinHTML = _driver.FindElement(
                By.Id("cotacaoBitcoin"));
            if (cotacaoBitcoinHTML != null)
            {
                cotacao = new CotacaoBitcoin();
                cotacao.NomeMoeda = "BITCOIN";
                cotacao.UltimaAtualizacao = DateTime.Now;
                cotacao.VlCotacaoDolar = Convert.ToDouble(
                    cotacaoBitcoinHTML.Text);
            }

            return cotacao;
        }

        public void Fechar()
        {
            _driver.Quit();
            _driver = null;
        }
    }
}