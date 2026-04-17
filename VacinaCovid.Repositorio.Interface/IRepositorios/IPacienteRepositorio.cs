using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacinaCovid.Entidade.Entidades;
using VacinaCovid.Entidade.EntidadesDTO;

namespace VacinaCovid.Repositorio.Interface.IRepositorios
{
    public interface IPacienteRepositorio
    {
        void Adicionar(Paciente paciente);
        List<Paciente> Listar();
        Paciente ObterId(int id);
        void Atualizar(string paciente, string pacienteAtualizado);
        void Remover(int id);
    }
}
