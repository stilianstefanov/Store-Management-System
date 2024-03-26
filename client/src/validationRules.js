export const commonValidationRules = {
    email: {
      pattern: /^[^@\s]+@[^@\s]+\.[^@\s]+$/,
      message: 'Please enter a valid email address.',
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