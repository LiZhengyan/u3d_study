

window_Prototype={x=0,y=0,width=100,height=100}

myWin={title="Hello"}

aa=setmetatable(myWin, { __index= window_Prototype })

for k,v in pairs(myWin) do
    print( k,v)
end
--在这里循环的时候 只有title。  访问的时候才能访问到

print (myWin.width) 
print(myWin)   
print(aa)

-- setmetatable 返回的是第一个参数