﻿using HM.Core.Messages;

namespace HM.Clientes.API.Application.Events
{
    public class ContatoRegistradoEvent : Event
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }

        public ContatoRegistradoEvent(Guid id, string nome)
        {
            AggregateId = id;
            Id = id;
            Nome = nome;
        }
    }
}