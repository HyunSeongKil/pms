package dev.hyunlab.pms.controller.rest;

import java.io.File;
import java.util.HashMap;
import java.util.Map;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.CrossOrigin;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;

import dev.hyunlab.pms.service.FileService;

@CrossOrigin(origins = "*")
@RestController
@RequestMapping("/api/files")
public class FileRestController {

    @Autowired
    private FileService service;
    
    @GetMapping()
    public ResponseEntity<Map<String,Object>> files(@RequestParam("dir") String dir){
        if(0 == dir.length()){
            dir = "." + File.separator;
        }

        dir = dir.replaceAll("/", "\\"+File.separator);

        Map<String,Object> map = new HashMap<>();
        map.put("data", service.getFiles(dir));
        map.put("dir", dir);

        return ResponseEntity.ok(map);
    }
}
