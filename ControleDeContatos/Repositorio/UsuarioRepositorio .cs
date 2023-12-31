﻿using ControleDeContatos.Data;
using ControleDeContatos.Helpers;
using ControleDeContatos.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ControleDeContatos.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly DataContext _dataContext;

        public UsuarioRepositorio(DataContext dataContext) 
        {
            _dataContext = dataContext;
        }

        public UsuarioModel ListarPorId(int id)
        {
            return _dataContext.Usuarios.FirstOrDefault(x => x.Id == id);
        }

        public UsuarioModel BuscarPorEmailLogin(string email, string login)
        {
            return _dataContext.Usuarios.FirstOrDefault(x => x.Email.ToUpper() == email.ToUpper() && x.Login.ToUpper() == login.ToUpper());
        }

        public List<UsuarioModel> BuscarUsuarios()
        {
            return _dataContext.Usuarios
                .Include(x => x.Contatos)
                .ToList();
        }

        public UsuarioModel Adicionar(UsuarioModel usuario)
        {
            usuario.DataCadastro = DateTime.Now;
            usuario.SetPasswordHash();
            _dataContext.Usuarios.Add(usuario);
            _dataContext.SaveChanges();
            return usuario;
        }

        public UsuarioModel Atualizar(UsuarioModel usuario)
        {
            UsuarioModel usuarioDb = ListarPorId(usuario.Id);

            if (usuarioDb == null)
            {
                throw new System.Exception("Houve um erro ao atualizar o usuário!");
            }

            usuarioDb.Name = usuario.Name;
            usuarioDb.Email = usuario.Email;
            usuarioDb.Login = usuario.Login;
            usuarioDb.Perfil = usuario.Perfil;
            usuarioDb.DataAtualizacao = DateTime.Now;

            _dataContext.Usuarios.Update(usuarioDb);
            _dataContext.SaveChanges();

            return usuarioDb;
        }

        public UsuarioModel AlterarSenha(AlterarSenhaModel alterarSenhaModel)
        {
            UsuarioModel usuarioDb = ListarPorId(alterarSenhaModel.Id);
            
            if(usuarioDb == null)
            {
                throw new Exception("Houve um erro na atualização da senha, usuario não encontrado!"); 
            }

            if (!usuarioDb.PasswordValid(alterarSenhaModel.SenhaAtual))
            {
                throw new Exception("Senha atual não confere!");
            }

            if (usuarioDb.PasswordValid(alterarSenhaModel.NovaSenha))
            {
                throw new Exception("Nova senha deve ser diferente da senha atual!");
            }

            usuarioDb.setNewPassword(alterarSenhaModel.NovaSenha);
            usuarioDb.DataAtualizacao = DateTime.Now;

            _dataContext.Usuarios.Update(usuarioDb);
            _dataContext.SaveChanges();

            return usuarioDb;
        }

        public bool Apagar(int id)
        {
            UsuarioModel usuarioDb = ListarPorId(id);

            if (usuarioDb == null)
            {
                throw new System.Exception("Houve um erro na exclusão do usuário!");
            }

            _dataContext.Usuarios.Remove(usuarioDb);
            _dataContext.SaveChanges();

            return true;
        }

        public UsuarioModel LoginUsuario(string login)
        {
            return _dataContext.Usuarios.FirstOrDefault(x => x.Login.ToUpper() == login.ToUpper());
        }
    }
}
