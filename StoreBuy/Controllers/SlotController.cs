using StoreBuy.Domain;
using StoreBuy.Repositories;
using StoreBuy.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace StoreBuy.Controllers
{
    [RoutePrefix("api/Slot")]
    public class SlotController : ApiController
    {

        IStoreFilledSlotRepository SlotRepository = null;
        IStoreSearchRepository StoreRepository=null;
        
        public SlotController(IStoreFilledSlotRepository SlotRepository,
            IStoreSearchRepository StoreRepository)
        {
            this.StoreRepository = StoreRepository;
            this.SlotRepository = SlotRepository;           
        }

        [HttpGet]
        [Route("CheckIfSlotFilled")]
        public bool CheckIfSlotFilled(long StoreId,string SlotDate,string SlotTime)
        {
            StoreInfo Store = StoreRepository.GetById(StoreId); 
            bool IsSlotFilled= SlotRepository.CheckIfStoreSlotFilled(Store,SlotTime, SlotDate);
            return IsSlotFilled;
        }
    
    }
}
