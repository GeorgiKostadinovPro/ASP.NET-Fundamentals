namespace Library.Data.Common.Constants
{
    public static class ValidationalConstants
    {
        public static class CategoryConstants
        {
            public const int CategoryNameMinLength = 5;
            public const int CategoryNameMaxLength = 50;
        }

        public static class BookConstants
        {
            public const int BookTitleMinLength = 10;
            public const int BookTitleMaxLength = 50;

            public const int BookAuthorMinLength = 5;
            public const int BookAuthorMaxLength = 50;

            public const int BookDescriptionMinLength = 5;
            public const int BookDescriptionMaxLength = 5000;

            public const double BookRatingMinValue = 0.0d;
            public const double BookRatingMaxValue = 10.0d;
        }
    }
}
