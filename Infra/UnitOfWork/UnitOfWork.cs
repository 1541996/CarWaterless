using Core.Interface;
using Infra.Models;
using Infra.Repository;

namespace Infra.UnitOfWork
{
    public class UnitOfWork
    {
        private IRepository<tbAdmin> _adminRepo;
        private IRepository<tbBranch> _branchRepo;
        private IRepository<tbCarCategory> _carCategoryRepo;
        private IRepository<tbChatMessage> _chatMessageRepo;
        private IRepository<tbCustomer> _customerRepo;
        private IRepository<tbCustomerVehicle> _customerVehicleRepo;
        private IRepository<tbDiscount> _discountRepo;
        private IRepository<tbDiscountedCar> _discountedCarRepo;
        private IRepository<tbFinance> _financeRepo;
        private IRepository<tbNotification> _notificationRepo;
        private IRepository<tbOperation> _operationRepo;
        private IRepository<tbRank> _rankRepo;
        private IRepository<tbAdditionalService> _serviceCategoryRepo;
      
        private IRepository<tbTopCustomer> _topCustomerRepo;
        private IRepository<tbTownship> _townshipRepo;
        private IRepository<tbPhoto> _photoRepo;

        private IRepository<tbAdditionalService> _additionalServiceRepo;
        private IRepository<tbFeedBack> _feedbackRepo;
        private IRepository<tbMemberPackage> _memberPackageRepo;
        private IRepository<tbAdvertisement> _adsRepo;
        private IRepository<tbDailyHot> _dailyHotRepo;





        private CarWaterLessContext _dbContext;
        public UnitOfWork(CarWaterLessContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IRepository<tbDailyHot> dailyHotRepo
        {
            get
            {
                if (_dailyHotRepo == null)
                {
                    _dailyHotRepo = new Repository<tbDailyHot>(_dbContext);
                }
                return _dailyHotRepo;
            }
        }

        public IRepository<tbAdvertisement> adsRepo
        {
            get
            {
                if (_adsRepo == null)
                {
                    _adsRepo = new Repository<tbAdvertisement>(_dbContext);
                }
                return _adsRepo;
            }
        }


        public IRepository<tbMemberPackage> memberPackageRepo
        {
            get
            {
                if (_memberPackageRepo == null)
                {
                    _memberPackageRepo = new Repository<tbMemberPackage>(_dbContext);
                }
                return _memberPackageRepo;
            }
        }

        public IRepository<tbFeedBack> feedbackRepo
        {
            get
            {
                if (_feedbackRepo == null)
                {
                    _feedbackRepo = new Repository<tbFeedBack>(_dbContext);
                }
                return _feedbackRepo;
            }
        }

        public IRepository<tbOperation> operationRepo
        {
            get
            {
                if (_operationRepo == null)
                {
                    _operationRepo = new Repository<tbOperation>(_dbContext);
                }
                return _operationRepo;
            }
        }

        public IRepository<tbAdditionalService> additionalServiceRepo
        {
            get
            {
                if (_additionalServiceRepo == null)
                {
                    _additionalServiceRepo = new Repository<tbAdditionalService>(_dbContext);
                }
                return _additionalServiceRepo;
            }
        }

        public IRepository<tbAdmin> adminRepo
        {
            get
            {
                if (_adminRepo == null)
                {
                    _adminRepo = new Repository<tbAdmin>(_dbContext);
                }
                return _adminRepo;
            }
        }
        public IRepository<tbBranch> branchRepo
        {
            get
            {
                if (_branchRepo == null)
                {
                    _branchRepo = new Repository<tbBranch>(_dbContext);
                }
                return _branchRepo;
            }
        }
        public IRepository<tbTownship> townshipRepo
        {
            get
            {
                if (_townshipRepo == null)
                {
                    _townshipRepo = new Repository<tbTownship>(_dbContext);
                }
                return _townshipRepo;
            }
        }
        public IRepository<tbCarCategory> carCategoryRepo
        {
            get
            {
                if (_carCategoryRepo == null)
                {
                    _carCategoryRepo = new Repository<tbCarCategory>(_dbContext);
                }
                return _carCategoryRepo;
            }
        }
        public IRepository<tbChatMessage> chatMessageRepo
        {
            get
            {
                if (_chatMessageRepo == null)
                {
                    _chatMessageRepo = new Repository<tbChatMessage>(_dbContext);
                }
                return _chatMessageRepo;
            }
        }
        public IRepository<tbCustomer> customerRepo
        {
            get
            {
                if (_customerRepo == null)
                {
                    _customerRepo = new Repository<tbCustomer>(_dbContext);
                }
                return _customerRepo;
            }
        }
        public IRepository<tbCustomerVehicle> customerVehicleRepo
        {
            get
            {
                if (_customerVehicleRepo == null)
                {
                    _customerVehicleRepo = new Repository<tbCustomerVehicle>(_dbContext);
                }
                return _customerVehicleRepo;
            }
        }
        public IRepository<tbDiscount> discountRepo
        {
            get
            {
                if (_discountRepo == null)
                {
                    _discountRepo = new Repository<tbDiscount>(_dbContext);
                }
                return _discountRepo;
            }
        }
        public IRepository<tbDiscountedCar> discountedCarRepo
        {
            get
            {
                if (_discountedCarRepo == null)
                {
                    _discountedCarRepo = new Repository<tbDiscountedCar>(_dbContext);
                }
                return _discountedCarRepo;
            }
        }
        public IRepository<tbFinance> financeRepo
        {
            get
            {
                if (_financeRepo == null)
                {
                    _financeRepo = new Repository<tbFinance>(_dbContext);
                }
                return _financeRepo;
            }
        }
        public IRepository<tbNotification> notificationRepo
        {
            get
            {
                if (_notificationRepo == null)
                {
                    _notificationRepo = new Repository<tbNotification>(_dbContext);
                }
                return _notificationRepo;
            }
        }

        public IRepository<tbPhoto> photoRepo
        {
            get
            {
                if (_photoRepo == null)
                {
                    _photoRepo = new Repository<tbPhoto>(_dbContext);
                }
                return _photoRepo;
            }
        }

        //public IRepository<tbCollectionChannel> collectionChannelRepo
        //{
        //    get
        //    {
        //        if (_collectionChannelRepo == null)
        //        {
        //            _collectionChannelRepo = new Repository<tbCollectionChannel>(_dbContext);
        //        }
        //        return _collectionChannelRepo;
        //    }
        //}
        //public IRepository<tbDiscount> discountRepo
        //{
        //    get
        //    {
        //        if (_discountRepo == null)
        //        {
        //            _discountRepo = new Repository<tbDiscount>(_dbContext);
        //        }
        //        return _discountRepo;
        //    }
        //}
        //public IRepository<tbGenre> genreRepo
        //{
        //    get
        //    {
        //        if (_genreRepo == null)
        //        {
        //            _genreRepo = new Repository<tbGenre>(_dbContext);
        //        }
        //        return _genreRepo;
        //    }
        //}
        //public IRepository<tbItem> itemRepo
        //{
        //    get
        //    {
        //        if (_itemRepo == null)
        //        {
        //            _itemRepo = new Repository<tbItem>(_dbContext);
        //        }
        //        return _itemRepo;
        //    }
        //}
        //public IRepository<tbItemCategory> itemcategoryRepo
        //{
        //    get
        //    {
        //        if (_itemcategoryRepo == null)
        //        {
        //            _itemcategoryRepo = new Repository<tbItemCategory>(_dbContext);
        //        }
        //        return _itemcategoryRepo;
        //    }
        //}
        //public IRepository<tbOrder> orderRepo
        //{
        //    get
        //    {
        //        if (_orderRepo == null)
        //        {
        //            _orderRepo = new Repository<tbOrder>(_dbContext);
        //        }
        //        return _orderRepo;
        //    }
        //}
        //public IRepository<tbOrderDetail> orderdetailRepo
        //{
        //    get
        //    {
        //        if (_orderdetailRepo == null)
        //        {
        //            _orderdetailRepo = new Repository<tbOrderDetail>(_dbContext);
        //        }
        //        return _orderdetailRepo;
        //    }
        //}
        //public IRepository<tbPrice> priceRepo
        //{
        //    get
        //    {
        //        if (_priceRepo == null)
        //        {
        //            _priceRepo = new Repository<tbPrice>(_dbContext);
        //        }
        //        return _priceRepo;
        //    }
        //}
        //public IRepository<tbUser> userRepo
        //{
        //    get
        //    {
        //        if (_userRepo == null)
        //        {
        //            _userRepo = new Repository<tbUser>(_dbContext);
        //        }
        //        return _userRepo;
        //    }
        //}
        //public IRepository<tbMessage> messageRepo
        //{
        //    get
        //    {
        //        if(_messageRepo==null)
        //        {
        //            _messageRepo = new Repository<tbMessage>(_dbContext);
        //        }
        //        return _messageRepo;
        //    }
        //}
        //public IRepository<tbPhoto> photoRepo
        //{
        //    get
        //    {
        //        if (_photoRepo == null)
        //        {
        //            _photoRepo = new Repository<tbPhoto>(_dbContext);
        //        }
        //        return _photoRepo;
        //    }
        //}

        //public IRepository<tbReview> reviewRepo
        //{
        //    get
        //    {
        //        if(_reviewRepo==null)
        //        {
        //            _reviewRepo = new Repository<tbReview>(_dbContext);
        //        }
        //        return _reviewRepo;
        //    }
        //}



    }
}
