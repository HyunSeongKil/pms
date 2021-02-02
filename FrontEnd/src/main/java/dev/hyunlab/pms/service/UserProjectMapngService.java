package dev.hyunlab.pms.service;

import java.util.List;

import dev.hyunlab.pms.model.User;
import dev.hyunlab.pms.model.UserProjectMapng;


/**
 * 사용자 프로젝트 매핑
 */
public interface UserProjectMapngService {
    /**
     * 사용자 아이디로 프로젝트 목록 조회
     * @param userId
     * @return
     */
    List<UserProjectMapng> findByUserId(String userId);
}
