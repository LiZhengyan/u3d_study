--匿名函数
foo=function(x) return x end

print (foo)

--Table1
myHash={name="age" ,age=37, handsome=true}
print(myHash.name)

myHash.website="163.com"
print (myHash)

print (myHash.website)
print (myHash.handsome)


--Table2, 

t={['name']="luck",['city']='ChenHao'}

print(t['city'])

--用这种方式来遍历tab
for k,v in pairs(t) do
    print(k,v)
end

--数组
arr1={10,20,30,40,50}
print(arr1)
print(arr1[1])


--不同类型的数组
--注意下标不是从0开始。从1开始的
arr2={myHash,100,foo}
print(arr2[1],arr2[2],arr2[3])   