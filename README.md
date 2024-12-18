<H1>Desktop Calculator</H1>

### **1. Windows Forms UI Design**  
- **Form Design:**  
  Designed an intuitive graphical user interface using Windows Forms. This involved laying out buttons for digits (0–9), arithmetic operations (addition, subtraction, multiplication, division), and advanced functions like percentage or square root. The design also included a display textbox for users to input expressions and view results.  

  - **Button Layouts:** Organized buttons into a grid-like structure for logical grouping, enhancing usability. For instance, arithmetic operators are positioned on one side for quick access, while numbers are centrally placed.  
  - **Responsive Design:** Ensured that the calculator's UI adapts to window resizing or scaling for better user experience on different screen resolutions.  
  - **Isolated Views:** The Calculator's advance funtions and basic functions are separated by different tabs to provide better user-experience.
    
- **Event Handling:**  
  Implemented **event handlers** for each button, ensuring smooth interaction between user input and the underlying logic. For example:  
  - Clicking a digit appends it to the textbox.  
  - Clicking an operator updates the current operation while maintaining the previous input for multi-step calculations.  
  - The logic is also implemented in a robust way such that it can handle the complex expressions. such as `x` sign would be automatically added if a trignometric function is used after a number. `Ex:- 4xSin(60)`  
---

### **2. Expression Evaluation Library**  
- **Custom Parsing Logic:**  
  A significant highlight of project is the custom-built library for parsing and evaluating mathematical expressions. Unlike a simple sequential calculation, your library:  
  - Analyzes the expression string entered by the user. Converts the user-entered string `(e.g., 12 + 3 * 4)` into a format suitable for computation.
  - Identifies numbers, operators, and parentheses for processing.  

- **Operator Precedence and Associativity:**  
  The library respects **operator precedence** (e.g., multiplication and division are evaluated before addition and subtraction). For example:  
  - An expression like `2 + 3 * 4` yields `14`, not `20`.  
  - Associativity rules ensure left-to-right evaluation for operations of equal precedence.  

- **Error Handling in Parsing:**  
  Included mechanisms to detect and handle malformed expressions, such as mismatched parentheses or consecutive operators. These safeguards prevent runtime errors and improve user feedback.  

---

### **3. Error Handling**  
- **Input Validation:**  
  Implemented checks to prevent invalid inputs, such as:  
  - Multiple decimal points in a single number (e.g., `12.3.4` is disallowed).  
  - Consecutive operators like `++` or `*/`.  
  - Starting or ending the expression with an operator (e.g., `+123` or `123/`).  

- **Division by Zero:**  
  Recognized as a critical edge case, you included specific logic to handle division by zero gracefully. For example, instead of crashing, the calculator displays an appropriate error message like "Cannot divide by zero."  

- **General Exception Management:**  
  Wrapped computational logic in try-catch blocks to handle unforeseen errors. This ensures the application doesn't crash, maintaining a seamless user experience.  

---

### **4. Advanced Features**  
- **Scientific Functions:**  
  Added support for advanced operations like:  
  - **Exponentiation:** Allowing users to compute powers, such as `2^3 = 8`.  
  - **Square Root:** Direct computation of square roots (e.g., √16 = 4).
  - **Trignometric Functions:** Calculation of complex trignometric functions. Such as `Tan`, `Cos`, and `Sin` etc.
  - These functions enhance the utility of your calculator, making it suitable for basic scientific computations.  

- **Memory Storage:**  
  Introduced a **memory feature** to store and recall values during multi-step calculations.  
  - For example: Users can store an intermediate result, use it in further calculations, and retrieve it without re-entering the value manually.  
  - Memory operations include "M+", "M-", "MR" (memory recall), "MS" (memory store) and "MC" (memory clear).  

---

### **5. Code Structure and Modularity**  
- **Separation of Concerns:**  
  The project adheres to the principle of separating UI logic from computational logic:  
  - **UI Code:** Handles rendering and user interactions, such as button clicks and textbox updates.  
  - **Library Code:** Encapsulates the logic for expression evaluation, ensuring the core functionality is isolated and reusable.  

- **Reusable Components:**  
  Designed the expression evaluation library as a standalone module, making it possible to integrate into other projects requiring mathematical computation. This modularity improves code maintainability and readability.  

- **Modularity:**
  - The expression evaluation logic is modular, allowing integration with other C# projects requiring similar functionality.
  - Includes utility functions for parsing, precedence handling, and computation in modules.
    
- **Scalability:**  
  By organizing code into distinct functions and classes, you made it easy to add new features (e.g., additional scientific functions) without disrupting existing functionality.  

---

ADVANCE CALCULATOR
![Advance Calc](https://github.com/Surbhi-sinha/desktop_calculator/blob/main/advcalc.png)

<hr>

BASIC CALCULATOR
![Basic Calc](https://github.com/Surbhi-sinha/desktop_calculator/blob/main/basecalc.png)
