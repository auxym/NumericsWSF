module NumericsWSF

open ExcelDna.Integration
open MathNet.Numerics.Interpolation

[<ExcelFunction(Description="Linear Interpolation")>]
let LININTERP (x1:float[]) (y1:float[]) (x2:float[]) =
    let interp = LinearSpline.Interpolate(x1, y1)
    Array.map interp.Interpolate x2
