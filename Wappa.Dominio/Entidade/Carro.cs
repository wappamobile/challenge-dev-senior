﻿namespace Wappa.Dominio.Entidade
{
    public class Carro
    {
        public int Id { get; set; }
        public int IdMotorista { get; set; }
        public string Modelo { get; set; }
        public string Cor { get; set; }
        public string Marca { get; set; }
        public string Placa { get; set; }
        public int? Lugar { get; set; }
        public int? Mala { get; set; }

        public Motorista Motorista { get; set; }
    }
}