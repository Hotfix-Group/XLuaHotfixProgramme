
ResourcesManager={}

local function Load(path)
    local resObj=CS.UnityEngine.Resources.Load(path)
    return resObj
end

ResourcesManager.Load=Load

return ResourcesManager
