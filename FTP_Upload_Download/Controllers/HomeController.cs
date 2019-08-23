using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security;
using System.IO;
using FTP_Upload_Download.Entities;

namespace FTP_Upload_Download.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Upload(string txtUsuario, string txtSenha, string txtEnderecoServidorFTP, string txtArquivoUpload, string btnProcurar)
        {
            if (validaInformacaoServidorFTP(txtUsuario, txtSenha, txtEnderecoServidorFTP))
            //if(true)
            {
                if (!string.IsNullOrEmpty(txtArquivoUpload))
                {
                    string urlArquivoEnviar = txtEnderecoServidorFTP + "/" + Path.GetFileName(txtArquivoUpload);
                    try
                    {
                        FTP.EnviarArquivoFTP(txtArquivoUpload, urlArquivoEnviar, txtUsuario, txtSenha);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Erro " + ex.Message);
                    }
                }
            }
            else
            {
                Console.WriteLine("Informações do sevidor incompletas");
            }
            return View("Index");
        }

        public ActionResult Download(string txtUsuario, string txtSenha, string txtEnderecoServidorFTP, string txtArquivoDownload, string txtBaixarPara)
        {
            if (validaInformacaoServidorFTP(txtUsuario, txtSenha, txtEnderecoServidorFTP))
            {
                if (validaInformacaoDownload(txtArquivoDownload, txtBaixarPara))
                {
                    try
                    {
                        FTP.BaixarArquivoFTP(txtArquivoDownload, txtBaixarPara, txtUsuario, txtSenha);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Erro " + ex.Message);
                    }
                }
                else
                {
                    Console.WriteLine("Informações para download incompletas");
                }
            }
            else
            {
                Console.WriteLine("Informações do sevidor incompletas");
            }
            return View("Index");
        }

        private bool validaInformacaoServidorFTP(string txtUsuario, string txtSenha, string txtEnderecoServidorFTP)
        {
            if (string.IsNullOrEmpty(txtUsuario) || string.IsNullOrEmpty(txtSenha) || string.IsNullOrEmpty(txtEnderecoServidorFTP))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private bool validaInformacaoDownload(string txtArquivoDownload, string txtBaixarPara)
        {
            if (string.IsNullOrEmpty(txtArquivoDownload) || string.IsNullOrEmpty(txtBaixarPara))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}