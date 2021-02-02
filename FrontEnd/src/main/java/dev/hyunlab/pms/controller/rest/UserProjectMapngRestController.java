package dev.hyunlab.pms.controller.rest;

import java.util.HashMap;
import java.util.Map;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.CrossOrigin;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;

import dev.hyunlab.pms.model.User;
import dev.hyunlab.pms.service.UserProjectMapngService;

@CrossOrigin(origins = "*")
@RestController
@RequestMapping("/api/user_project_mapngs")
/**
 * 사용자 프로젝트 매핑
 */
public class UserProjectMapngRestController {

    @Autowired
    private UserProjectMapngService service;
    
    /**
     * 사용자 아이디로 프로젝트 목록 조회
     * @param userId
     * @return
     */
    @GetMapping()
    public ResponseEntity<Map<String,Object>> gets(@RequestParam("userId") String userId){
        
        Map<String,Object> map = new HashMap<>();
        map.put("data", service.findByUserId(userId));

        return ResponseEntity.ok(map);

    }
}
