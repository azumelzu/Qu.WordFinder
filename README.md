# 🧩 Word Finder Algorithm

An C# algorithm to find and count occurrences of words in a string matrix (as a `IEnumerable<string>`), searching **horizontally** and **vertically**.

---

## 🚀 Features

- ✅ Count occurrences of each word
- ✅ Scans both **rows (horizontal)** and **columns (vertical)**
- ✅ Clean, testable, and memory-efficient implementation

---

## 🧠 Algorithm Overview

- The algorithm is implemented using a recursive function. 
- Each recursive execution reduce matrix by looping over the rows and taking the first and last columns
- Then, for first and last (vertical stream) it counts how many times words in words stream appear, using substring matching (`IndexOf`)
- For the very first execution of the recursive call and the for loop, it seraches for horizontal words, also using substring matching (`IndexOf`)
- It restuns the top 10 most repeated words in the matrix, deduping words from words stream, ordering them by frequency and then alphabetically
---

## 🧪 Example

**Input Matrix:**
```csharp
string[] matrix = {
    "catidc",
    "lkmcat",
    "ratdog"
};
```

**Input Words:**
```csharp
string[] words = { "cat", "rat", "dog", "bird" };
```

**Output:**
- cat
- dog
- rat

Where cat appears two times, dog and rat one time. Bird is excluded as it doesn't appear in the matrix



