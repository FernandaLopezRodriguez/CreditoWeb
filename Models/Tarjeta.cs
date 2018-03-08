using System;
using System.ComponentModel.DataAnnotations;

namespace CreditoWeb.Models
{
    public class Tarjeta
    {
        [Required(ErrorMessage = "El número de tarjeta es necesario.")]
        //[CreditCard]
        public string TarjetaNum { get; set; }
        public TipoTarjeta TipoTarjeta { get; set; }

        public bool Valida { get; set; }
     
        public Tarjeta(string tarjetaNum)
        {
            this.TarjetaNum = tarjetaNum;
            Valida = esValida();
            TipoTarjeta = tipoDeTarjeta();            
        }


        /// Basado en el algoritmo de Luhn determinar si esta tarjeta es valida
        /// como estamos dentro de la clase de tarjeta tenemos acceso a la propiedad TarjetaNum 
        private bool esValida()
        {
            int suma=0, par=0;
            for (int i=1; i<=TarjetaNum.Length; i+=2)
            {
                string aux = TarjetaNum.Substring(i,1);
                par = Int32.Parse (aux);
                if (par >=10)
                {
                    suma+=par-9;
                }
                else{
                    suma+=par;
                }
            }
            for (int n=0; n<=TarjetaNum.Length; n+=2)
            {
                int impar = Int32.Parse (TarjetaNum.Substring(n,1));
                suma+=impar;
            }
            Valida = ( suma % 10 == 0);
            return Valida;
        }


        /// Si la tarjeta es valida determinar de cuál tipo es VISA, MASTERCARD, AMERICANEXPRESS
        /// como estamos dentro de la clase de tarjeta tenemos acceso a la propiedad TarjetaNum 
        private TipoTarjeta tipoDeTarjeta()
        {
            int inicio = (Int32.Parse(TarjetaNum.Substring(0,2)));
            int v = (Int32.Parse(TarjetaNum.Substring(0,1)));
            if (TarjetaNum.Length==15&&(inicio==34||inicio==37))
            {
            return TipoTarjeta.AMERICANEXPRESS;
            }            
            else if (TarjetaNum.Length==16&&(inicio==51||inicio==52||inicio==53||inicio==54||inicio==55))
            {
                return TipoTarjeta.MASTERCARD;
            }
            else if ((TarjetaNum.Length==13||TarjetaNum.Length==16)&&v==4)
            {
                return TipoTarjeta.VISA;
            }
            else
            {
                return TipoTarjeta.NOVALIDA;
            }
        }



    }

    public enum TipoTarjeta
    {
        VISA,
        MASTERCARD,
        AMERICANEXPRESS,
        NOVALIDA


    }
}