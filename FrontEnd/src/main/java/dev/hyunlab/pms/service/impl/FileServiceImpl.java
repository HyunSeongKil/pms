package dev.hyunlab.pms.service.impl;

import java.nio.file.Files;
import java.nio.file.Paths;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Date;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import org.springframework.beans.factory.annotation.Value;
import org.springframework.stereotype.Service;

import dev.hyunlab.pms.model.File;
import dev.hyunlab.pms.service.FileService;
import lombok.extern.slf4j.Slf4j;

@Service
@Slf4j
public class FileServiceImpl implements FileService {

    @Value("${app.base.dir}")
    private String baseDir;

    @Override
    public List<File> getFiles(String dir) {
        List<File> list = new ArrayList<>();

        java.io.File[] files = Paths.get(baseDir, dir).toFile().listFiles();

        
        if(null == files){
            return list;
        }
        

        SimpleDateFormat formater = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss");

        for(java.io.File f : files){
            File file = File.builder()
                            .date(formater.format(new Date(f.lastModified())))
                            .isDirectory(f.isDirectory())
                            .name(f.getName())
                            .size(f.length())
                            .build();
            list.add(file);
        }


        return list;
    }
    
}
