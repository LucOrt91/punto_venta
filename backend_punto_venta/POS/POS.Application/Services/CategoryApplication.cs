using AutoMapper;
using POS.Application.Commons.Bases;
using POS.Application.Dtos.Request;
using POS.Application.Dtos.Response;
using POS.Application.Interfaces;
using POS.Application.Validators.Category;
using POS.Domain.Entities;
using POS.Infrastructure.Commons.Bases.Request;
using POS.Infrastructure.Commons.Bases.Response;
using POS.Infrastructure.Persistences.Interfaces;
using POS.Utilities.Static;

namespace POS.Application.Services
{
    public class CategoryApplication : ICategoryApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly CategoryValidator _validationRules;

        public CategoryApplication(IUnitOfWork unitOfWork, IMapper mapper, CategoryValidator validationRules)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validationRules = validationRules;
        }
        public async Task<BaseResponse<BaseEntityResponse<CategoryResponseDto>>> ListCategories(BaseFilterRequest filters)
        {
            var response = new  BaseResponse < BaseEntityResponse <CategoryResponseDto>>();
            var categories = await _unitOfWork.Category.ListCategories(filters);

            if (categories is not null)
            {
                response.IsSucces = true;
                response.Data = _mapper.Map<BaseEntityResponse<CategoryResponseDto>>(categories);
                response.Message = ReplyMessage.MESSAGE_QUERY;
            }
            else
            {
                response.IsSucces = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
            }
            return response;
        } 
        public async Task<BaseResponse<IEnumerable<CategorySelectResponseDto>>> ListSelectCategories()
        {
            var response = new BaseResponse<IEnumerable<CategorySelectResponseDto>>();
            var categories = await _unitOfWork.Category.ListSelectCategories();

            if (categories is not null)
            {
                response.IsSucces = true;
                response.Data = _mapper.Map<IEnumerable<CategorySelectResponseDto>>(categories);
                response.Message = ReplyMessage.MESSAGE_QUERY;
            }
            else
            {
                response.IsSucces = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
            }
            return response;


        }
        public async Task<BaseResponse<CategoryResponseDto>> CategoryById(int categoryId)
        {
            var response = new BaseResponse<CategoryResponseDto>();
            var category = await _unitOfWork.Category.CategoryById(categoryId);

            if (category is not null)
            {
                response.IsSucces = true;
                response.Data = _mapper.Map<CategoryResponseDto>(category);
                response.Message = ReplyMessage.MESSAGE_QUERY;
            }
            else
            {
                response.IsSucces = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
            }
            return response;
        }
        public async Task<BaseResponse<bool>> RegisterCategory(CategoryRequestDto requestDto)
        {
            var response = new BaseResponse<bool>();
            var validatioResult = await _validationRules.ValidateAsync(requestDto);

            if (!validatioResult.IsValid)
            {
                response.IsSucces = false;
                response.Message = ReplyMessage.MESSAGE_VALIDATE;
                response.Errors = validatioResult.Errors;
                return response;
            }
            var category = _mapper.Map<Category>(requestDto);
            response.Data = await _unitOfWork.Category.RegisterCategory(category);

            if (response.Data)
            {
                response.IsSucces = true;
                response.Message = ReplyMessage.MESSAGE_SAVE;
            }
            else
            {
                response.IsSucces = false;
                response.Message = ReplyMessage.MESSAGE_FAILED;
            }
            return response;
        }

        public async Task<BaseResponse<bool>> EditCategory(int categoryId, CategoryRequestDto requestDto)
        {
            var response = new BaseResponse<bool>();
            var categoryEdit = await CategoryById(categoryId);

            if (categoryEdit.Data is null)
            {
                response.IsSucces = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
            }
            var category = _mapper.Map<Category>(requestDto);
            category.CategoryId = categoryId;
            response.Data = await _unitOfWork.Category.EditCategory(category);

            if (response.Data)
            {
                response.IsSucces = true;
                response.Message = ReplyMessage.MESSAGE_UPDATE;
            }
            else
            {
                response.IsSucces = false;
                response.Message = ReplyMessage.MESSAGE_FAILED;
            }
            return response;
        }           
      
        public async Task<BaseResponse<bool>> RemoveCategory(int categoryId)
        {
            var response = new BaseResponse<bool>();
            var category = await CategoryById(categoryId);

            if(category.Data is null)
            {
                response.IsSucces = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
            }
            response.Data = await _unitOfWork.Category.RemoveCategory(categoryId);

            if (response.Data)
            {
                response.IsSucces = true;
                response.Message = ReplyMessage.MESSAGE_DELETE;
            }
            else
            {
                response.IsSucces = false;
                response.Message = ReplyMessage.MESSAGE_FAILED;
            }
            return response;
        }
    }
}
