Правила валидации

CreateUserRequestDto
    Username
        - Обязательно заполнить
        - Длина от 4 до 10 символов
    Password
        - Обязательно заполнить
        - Длина минимум 16 символов

EmployeeDto
    Id
        - Обязательно заполнить
        - тип GUID
        - GUID отличный от пустого
    UserId
        - Обязательно заполнить
        - тип GUID
        - GUID отличный от пустого

LoginRequestDto
    Login
        - Обязательно заполнить
    Password
        - Обязательно заполнить