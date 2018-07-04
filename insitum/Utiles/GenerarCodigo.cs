using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace insitum.Utiles
{
    public static class GenerarCodigo
    {
        public static int Generar(int cuotaInferior, int cuotaSuperior)
        {
            var rdm = new Random();
            var numeroAleatorio = rdm.Next(cuotaInferior, cuotaSuperior);
            return numeroAleatorio;

        }
    }
}