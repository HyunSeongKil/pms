package dev.hyunlab.pms.controller.rest;

import java.util.HashMap;
import java.util.Map;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.CrossOrigin;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;

import dev.hyunlab.pms.service.UserService;

@CrossOrigin(origins = "*")
@RestController
@RequestMapping("/api/users")
public class UserRestController {

    @Autowired
    private UserService service;
    
    @GetMapping()
    public ResponseEntity<Map<String,Object>> findByUserId(@RequestParam String userId){
        Map<String,Object> map = new HashMap<>();
        map.put("data", service.findByUserId(userId));

        return ResponseEntity.ok(map);
    }


}
