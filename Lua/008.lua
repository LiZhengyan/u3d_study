Person = {} 

function Person:new(p)
    local obj = p
    self.myname = "ccct" 
    if obj == nil then 
        obj = { name = 'yufei', age = 27, handsome = true }
    end 
    self.__index = self
    return setmetatable(obj, self)
end 

function Person:toString()
    return self.name .. ":" ..self.age ..  ":" .. (self.handsome and "handsome" or "ugly")
end

me = Person:new()
for i ,v in pairs(me.__index) do
    print( i,v)
end
print("###")
--print (me.__index)
print "####"

me = Person:new()
for i ,v in pairs(me.__index) do
    print( i,v)
end

--print(me:toString())


kf = Person:new{ name = "King's fucking", age = 70, handsome = false}
print(kf:toString())

student=Person:new()

function student:new()
    newobj={year=2013}
    self.__index=self
    return setmetatable(newobj,self)
end

function student:toString()
    return "Student : ".. self.year.." : " .. self.name..":"..self.age    --age用的Person的
end  

--
s1=student:new()
print(s1:toString())

