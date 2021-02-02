package dev.hyunlab.pms.service;

import java.util.List;

import dev.hyunlab.pms.model.File;

public interface FileService {
    List<File> getFiles(String dir);
}
