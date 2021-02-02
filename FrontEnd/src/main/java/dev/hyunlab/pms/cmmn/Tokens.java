package dev.hyunlab.pms.cmmn;

import java.util.HashMap;
import java.util.Map;

import dev.hyunlab.pms.model.User;
import dev.hyunlab.pms.model.UserProjectMapng;

public class Tokens {

    public static Map<String, UserProjectMapng> map2 = new HashMap<>();
    

    public static void put(String token, UserProjectMapng mapng){
        map2.put(token, mapng);
    }

    public static UserProjectMapng get(String token){
        return map2.get(token);
    }

    public static void remove(String token){
        map2.remove(map2.get(token));
    }

}
