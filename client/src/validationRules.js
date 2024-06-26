export const commonValidationRules = {
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
    email: {
        pattern: /^[^@\s]+@[^@\s]+\.[^@\s]+$/,
        message: 'Please enter a valid email address.',
    },
    password: {
        pattern: /^(?=.*[A-Z])(?=.*\d)[A-Za-z\d]{6,50}$/,
        message: 'Password must be 6-50 characters long, include at least one uppercase letter, one lowercase letter, and one digit.'
    },
    confirmPassword: {
        message: 'Passwords do not match.'
    },
    userName: {
        minLength: 3,
        maxLength: 50
    },
    companyName: {
        minLength: 3,
        maxLength: 50
    }
};

export const clientValidationRules = {
    name: {
        minLength: 3,
        maxLength: 50
    },
    surname: {
        minLength: 0,
        maxLength: 50
    },
    lastName: {
        minLength: 3,
        maxLength: 50
    },
    currentCredit: {
        minValue: 0,
        maxValue: 99999
    },
    creditLimit: {
        minValue: 0,
        maxValue: 99999
    }
};

export const productValidationRules = {
    name: {
        minLength: 3,
        maxLength: 100
    },

    description: {
        minLength: 0,
        maxLength: 500
    },

    barcode: {
        minLength: 4,
        maxLength: 50
    },

    price: {
        minValue: 0.01,
        maxValue: 999999999
    },

    quantity: {
        minValue: 0,
        maxValue: 9999
    }
};

export const warehouseValidationRules = {
    name: {
        minLength: 3,
        maxLength: 100
    },

    type: {
        minLength: 3,
        maxLength: 100
    },
};