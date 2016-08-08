--MetaTable And  MetaMethod 


f_a={num=2,dem=3}
f_b={num=4,dem=7}

fra_op={}

function fra_op.__add(f1,f2)
    ret={}
    ret.num=f1.num*f2.dem +f2.num*f1.dem
    ret.dem=f1.dem*f2.dem
    ret.endnum= ret.num / ret.dem
    print(ret.num,ret.dem)
    return ret
end


function fra_op.__sub(f1,f2)
    ret={}
    ret.num=f1.num*f2.dem +f2.num*f1.dem
    ret.dem=f1.dem*f2.dem
    print(ret.num,ret.dem)
    return ret
end

setmetatable(f_a, fra_op)
setmetatable(f_b, fra_op)

print (10 - 9 ) 
print (10 + 9 )
--没有设置setmetatable的不受影响


--fra_s=f_a+f_b
--fra_s.__add()
--
----print (__add(f_a,f_b))
for i ,v in pairs(f_a + f_b) do
print("key->var==>",i,v)
end
print ((f_a+f_b).num / (f_a+f_b).dem )

for i ,v in pairs(f_a - f_b) do
    print(i,v)
    end

--理解起来有点困难，等于重写了加法和减法。_add代表加法 ——sub 代表减法。  重写了这2个。



t1 = {1,2,3,4}
t2 = {5,6,7,8,9}

--mt={}
--mt._add= function(a,b)