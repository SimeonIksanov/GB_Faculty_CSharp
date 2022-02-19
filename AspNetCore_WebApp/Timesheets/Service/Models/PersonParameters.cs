using System;
namespace Service.Models
{
    public class PersonParameters
    {
        const int MAX_PAGE_SIZE = 50;

        private int _pageSize = MAX_PAGE_SIZE;
        private int _pageNumber = 1;


        public int PageNumber
        {
            get
            {
                return _pageNumber;
            }
            set
            {
                _pageNumber = 0 < value
                        ? value
                        : 1;
            }
        }

        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = 0 < value && value <= MAX_PAGE_SIZE
                        ? value
                        : MAX_PAGE_SIZE;
            }
        }
    }
}
