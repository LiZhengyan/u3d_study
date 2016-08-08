function newCounter()
    local i = 0
    print("N=>",i)
    return function(a) print("t=>",i);i=i+a ;return i ;end
end

c1=newCounter()  
-- 这里的C1就是匿名函数本身。


print (c1(5))
print (c1(4))
print (c1(3))


function values (t)
    local i = 0
    return function() i = i + 1; return t[i] end
end
 
t = {10, 20, 30}
c2=values(t)

for element in  c2   do
    print(element)
end




