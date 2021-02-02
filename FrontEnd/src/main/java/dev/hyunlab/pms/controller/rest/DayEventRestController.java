package dev.hyunlab.pms.controller.rest;

import java.util.HashMap;
import java.util.List;
import java.util.Map;

import org.apache.commons.lang.StringUtils;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.CrossOrigin;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;

import dev.hyunlab.pms.model.DayEvent;
import dev.hyunlab.pms.service.DayEventService;
import lombok.extern.slf4j.Slf4j;

@CrossOrigin(origins = "*")
@RestController
@RequestMapping("/api/dayevents")
@Slf4j
public class DayEventRestController {

    @Autowired
    private DayEventService service;
    
    @GetMapping()
    public ResponseEntity<Map<String,Object>> get(@RequestParam int year, @RequestParam int month){
        
        String de = year + StringUtils.leftPad(""+month, 2, '0');

        Map<String,Object> map = new HashMap<>();
        List<DayEvent> list = service.findByDeStartingWith(de);
        list.forEach(x->{
            x.setEventCn("");
        });
        map.put("data", list);
        
        return ResponseEntity.ok(map);
    }
    
    
    @GetMapping("/{id}")
    public ResponseEntity<Map<String,Object>> getById(@PathVariable("id") long id){
        Map<String,Object> map = new HashMap<>();
        map.put("data", service.findById(id));

        return ResponseEntity.ok(map);
    }



    @PostMapping()
    public ResponseEntity<Map<String,Object>> post(@RequestBody DayEvent model){
        // DayEvent model = DayEvent.builder()
        //                 .de(param.get("de").toString())
        //                 .eventTitle(param.get("eventTitle").toString())
        //                 .eventCn(param.get("eventCn").toString())
        //                 .build();

        service.save(model);

        return ResponseEntity.ok().build();
    }
}
