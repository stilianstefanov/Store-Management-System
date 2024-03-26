export const commonValidationRules = {
    email: {
        pattern: /^[^@\s]+@[^@\s]+\.[^@\s]+$/,
        message: 'Please enter a valid email address.',
    },
    password: {
        pattern: /^(?=.*[A-Z])(?=.*\d)[A-Za-z\d]{6,50}$/,
        message: 'Password must be 6-50 characters long, include at least one uppercase letter, one lowercase letter, and one digit.'
    },
    required: (fieldName) => ({
        message: `The field ${fieldName} is required.`,
    }),
    length: (fieldName, minLength, maxLength) => ({
        message: `The field ${fieldName} must be between ${minLength} and ${maxLength} characters long.`,
    }),
    range: (fieldName, minValue, maxValue) => ({
        message: `The field ${fieldName} must be between ${minValue} and ${maxValue}.`,
    })
};

export const registerValidationRules = {
    userName: {
        minLength: 3,
        maxLength: 50
    },
    companyName: {
        minLength: 3,
        maxLength: 50
    }
};