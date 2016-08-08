function myPower(x)
    return function(y) return y^x end
end

print (myPower(4)(2))


function Mavg()
    return "lua",28
end

name,age = Mavg()

print(name,age)