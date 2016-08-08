function fib(n)
    if n<2  then return 1 end
    return fib(n-2)+fib(n-1)
end



print (fib(5))


function newCounter()
    local i = 0
    return function()     -- anonymous function
       i = i + 1
        return i
    end
end
 
c1 = newCounter()
print(c1())  --> 1
print(c1())  --> 2



