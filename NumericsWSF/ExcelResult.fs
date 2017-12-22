module ExcelResult

open ExcelDna.Integration

type ExcelResult = 
    | Value of float
    | Text of string
    | Error of ExcelError
    | ComplexVal of ExcelComplex.ExcelComplex

let valueError = Error ExcelError.ExcelErrorValue

let toObject (r:ExcelResult) = r :> obj