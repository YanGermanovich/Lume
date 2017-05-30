using BLL.Entities;
using BLL.Services_Interface;
using Lume.Infrastructure.Helper;
using Lume.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lume.Infrastructure.Mappers
{
    public static class StockMapper
    {
        public static BllStock ToBll(this StockViewModel stock)
        {
            return new BllStock()
            {
                BeginingDate = stock.BeginData,
                FinishingDate = stock.EndData,
                Description = stock.Description,
                Id_Author = stock.id_author,
                Id_StockType = stock.id_stockType,
                Id = stock.Id,
                Name = stock.Name
            };
        }

        public static StockViewModel ToMvc(this BllStock stock, IService<BllUser> userService, IService<BllImage> imageService, 
                                          IService<BllStockImage> stockImageService, IService<BllStockType> stockType, 
                                          IService<BllHistory> historyService, IService<BllAvatar> avatarService,
                                          IService<BllUserStock> userStockService, IService<BllStockProgress> stockProgressService,
                                          IService<BllStockPrize> stockPrizeService, IService<BllPrize> prizeService, string Email)
        {
            var allPrizes = stockPrizeService.GetAllByPredicate(ValueCompileVisitor.Convert<BllStockPrize>(sP => sP.Id_Stock == stock.Id)).ToList();
            var s= new StockViewModel()
            {
                BeginData = stock.BeginingDate,
                EndData = stock.FinishingDate,
                Description = stock.Description,
                id_author = stock.Id_Author,
                id_stockType = stock.Id_StockType,
                Id = stock.Id,
                Name = stock.Name,
                AuthorName = userService.GetEntitieById(stock.Id_Author).UserName,
                Image = GetImages(stock, userService, imageService, stockImageService, stockProgressService, Email),
                stockType = stockType.GetEntitieById(stock.Id_StockType).Name,
                AuthorAvatar = avatarService.GetEntitieById(userService.GetEntitieById(stock.Id_Author).Id_Avatar).Src,
                isMine = userService.GetFirstByPredicate(u => u.Email == Email).Id == stock.Id_Author,
                id_prize = allPrizes.Select(sP => sP.Id_Prize).ToList(),
                prizes = allPrizes.Select(sp => prizeService.GetEntitieById(sp.Id_Prize).Description).ToList(),
                Participants = userStockService.GetAllByPredicate(ValueCompileVisitor.Convert<BllUserStock>(uS => uS.Id_Stock == stock.Id)).Select(u => userService.GetEntitieById(u.Id_User).ToMvc(stockProgressService, stockImageService, stock.Id)).ToList()
            };
            s.isCompleted = !s.isMine && s.Participants.Any(sU => sU.Email == Email & sU.Scanned == s.Image.Count);
            if(s.isMine || s.isCompleted)
            {
                s.prizes_data = allPrizes.Select(sp => prizeService.GetEntitieById(sp.Id_Prize).Data).ToList();
            }
            
            s.isTakeParticapent = s.Participants.Any(sU => sU.Email == Email);
            return s;
        }

        public static List<StockImageViewModel> GetImages(BllStock stock, IService<BllUser> userService, 
                                                    IService<BllImage> imageService, IService<BllStockImage> stockImageService, 
                                                    IService<BllStockProgress> stockProgressService,  string Email)
        {
            var myId = userService.GetFirstByPredicate(ValueCompileVisitor.Convert<BllUser>(u => u.Email == Email)).Id;
            return stockImageService.GetAllByPredicate(ValueCompileVisitor.Convert<BllStockImage>(sI => sI.Id_Stock == stock.Id)).Select(sI => sI.ToMvc(stockProgressService,imageService,myId)).ToList();
        }
    }
}
