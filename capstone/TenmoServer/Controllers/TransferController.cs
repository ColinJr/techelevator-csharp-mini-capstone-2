﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.Models;
using TenmoServer.DAO;
using Microsoft.AspNetCore.Authorization;
using ShredClasses;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TenmoServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class TransferController : ControllerBase
    {

        private readonly ITransferDao _transferDao;
        private readonly IUserDao _userDao;

        public TransferController(ITransferDao transferDao, IUserDao userDao)
        {
            _transferDao = transferDao;
            _userDao = userDao;
        }

        // TODO: GET List of past transactions from user
        [HttpGet("{userId}")]
        [Authorize]
        public ActionResult<List<Transfer>> GetTransfers(int userId)
        {
            var user =_userDao.GetUser(User.Identity.Name);
            if(user == null || userId != user.UserId) 
            {
                return Forbid();
            }
            return _transferDao.GetTransfersForUser(userId);
        }
        [HttpGet("pending/{transferId}")]
        public ActionResult<IEnumerable<Transfer>> GetPendingTransfers(int transferId)
        {
            return _transferDao.GetPendingTransfers(transferId);
        }

        [HttpGet("user/{userId}")]
        public ActionResult<IEnumerable<Transfer>> GetTransfersByUser(int userId)
        {
            return _transferDao.GetTransfersForUser(userId);
        }
        [HttpGet("{transferId}")]
        public ActionResult<Transfer> GetTransferById(int transferId)
        {
            return _transferDao.GetTransferById(transferId);
        }
        [HttpPost("request")]

        public void PostTransferRequest(int senderId, int targetId, decimal amount)
        {
            _transferDao.RequestTransfer(senderId, targetId, amount);
        }
        //[HttpPost("pay")]

        //public void PostTransfer(int senderId, int targetId, decimal amount)
        //{
        //    _transferDao.SendTransfer(senderId, targetId, amount);
        //}


        [HttpPost("SendMoney")]
        public void PostTransfer(transfere_request transfere)
        {
            _transferDao.SendTransfer(transfere.sender_Id, transfere.target_Id, transfere._amount,transfere.IsThisASend);
             
        }



        // public void ApproveReject(Transfer _Transfer, int _AppRej)
        [HttpPost("AppRej")]
        public ActionResult<bool> ApproveReject(TransferAppRej transferApp)
        {
          return   _transferDao.ChangeTransferStatus(transferApp.Trans_id, transferApp.Action_id);
        }

    }
}
 